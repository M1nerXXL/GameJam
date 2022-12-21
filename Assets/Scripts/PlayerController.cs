using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public Rigidbody2D snowballRB;

    private bool doubleJump = false;
    public bool grounded;
    public bool inWall;
    private bool floorLaserL;
    private bool floorLaserR;
    public bool facingRight = true;
    public float input;
    public float walkingSpeed;
    public float jumpPower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Rays for wall detection
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.7f), Vector2.left * 0.36f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * 0.36f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.7f), Vector2.left * 0.36f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.7f), Vector2.right * 0.36f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * 0.36f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.7f), Vector2.right * 0.36f, Color.red);
        bool wallLaserTL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.7f), Vector2.left, 0.35f);
        bool wallLaserML = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 0.35f);
        bool wallLaserBL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.7f), Vector2.left, 0.35f);
        bool wallLaserTR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.7f), Vector2.right, 0.35f);
        bool wallLaserMR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 0.35f);
        bool wallLaserBR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.7f), Vector2.right, 0.35f);

        //Get movement input
        input = Input.GetAxis("Horizontal");

        //If wall on left, block left movement
        if ((wallLaserTL || wallLaserML || wallLaserBL) && input < 0)
        {
            input = 0;
            inWall = true;
        }
        //If wall on right, block right movement
        else if ((wallLaserTR || wallLaserMR || wallLaserBR) && input > 0)
        {
            input = 0;
            inWall = true;
        }
        else
        {
            inWall = false;
        }

        //Move
        playerRB.velocity = new Vector2(input * walkingSpeed, playerRB.velocity.y);
        //Rotate
        if (input > 0)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            facingRight = true;
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
            facingRight = false;
        }

        //Rays for floor detection
        if (facingRight)
        {
            Debug.DrawRay(new Vector2(transform.position.x - 0.275f, transform.position.y), Vector2.down * 0.8f, Color.red);
            floorLaserL = Physics2D.Raycast(new Vector2(transform.position.x - 0.275f, transform.position.y), Vector2.down, 0.8f);
            floorLaserR = false;
        }
        else
        {
            Debug.DrawRay(new Vector2(transform.position.x + 0.275f, transform.position.y), Vector2.down * 0.8f, Color.red);
            floorLaserR = Physics2D.Raycast(new Vector2(transform.position.x + 0.275f, transform.position.y), Vector2.down, 0.8f);
            floorLaserL = false;
        }

        //Check for floor
        if (floorLaserL || floorLaserR)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        //Enable double jump when grounded
        if (grounded)
        {
            doubleJump = true;
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (grounded || doubleJump))
        {
            if (!grounded)
            {
                doubleJump = false;
            }
            playerRB.velocity = (new Vector2(playerRB.velocity.x, 0));
            playerRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}