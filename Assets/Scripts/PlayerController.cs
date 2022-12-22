using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public Rigidbody2D snowballRB;

    private bool gameActive = true;
    private bool doubleJump = false;
    public bool grounded;
    public bool inWall;
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
        //Get movement input
        input = Input.GetAxis("Horizontal");

        //Rays for wall detection
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x - 0.35f, transform.position.y + 0.75f), Vector2.down * 1.45f, Color.blue);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x - 0.375f, transform.position.y + 0.75f), Vector2.down * 1.45f, Color.red);
        bool wallLaserCloseL = Physics2D.Raycast(new Vector2(transform.position.x - 0.35f, transform.position.y + 0.75f), Vector2.down, 1.48f);
        bool wallLaserFarL = Physics2D.Raycast(new Vector2(transform.position.x - 0.375f, transform.position.y + 0.75f), Vector2.down, 1.48f);

        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x + 0.35f, transform.position.y + 0.75f), Vector2.down * 1.45f, Color.blue);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x + 0.375f, transform.position.y + 0.75f), Vector2.down * 1.45f, Color.red);
        bool wallLaserCloseR = Physics2D.Raycast(new Vector2(transform.position.x + 0.35f, transform.position.y + 0.75f), Vector2.down, 1.48f);
        bool wallLaserFarR = Physics2D.Raycast(new Vector2(transform.position.x + 0.375f, transform.position.y + 0.75f), Vector2.down, 1.48f);

        //If wall on left, block left movement
        if (wallLaserFarL)
        {
            if (input < 0)
            {
                input = 0;
            }
            if (wallLaserCloseL)
            {
                transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
            }
        }
        //If wall on right, block right movement
        if (wallLaserFarR)
        {
            if (input > 0)
            {
                input = 0;
            }
            if (wallLaserCloseR)
            {
                transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y);
            }
        }

        //Rays for floor detection
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x - 0.275f, transform.position.y - 0.775f), Vector2.right * 0.550f, Color.red);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x - 0.275f, transform.position.y - 0.75f), Vector2.right * 0.550f, Color.blue);
        bool floorLaserFar = Physics2D.Raycast(new Vector2(transform.position.x - 0.275f, transform.position.y - 0.775f), Vector2.right, 0.550f);
        bool floorLaserClose = Physics2D.Raycast(new Vector2(transform.position.x - 0.275f, transform.position.y - 0.75f), Vector2.right, 0.550f);
        //Check for floor
        if (floorLaserFar)
        {
            grounded = true;
            playerRB.gravityScale = 0;
            if (floorLaserClose)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
            }
        }
        else
        {
            grounded = false;
            playerRB.gravityScale = 5;
        }

        //Move
        if (gameActive)
        {
            playerRB.velocity = new Vector2(input * walkingSpeed, playerRB.velocity.y);
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
        }

        //Enable double jump when grounded
        if (grounded)
        {
            doubleJump = true;
        }
        //Jump
        if (gameActive && Input.GetKeyDown(KeyCode.Space) && (grounded || doubleJump))
        {
            if (!grounded)
            {
                doubleJump = false;
            }
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
            playerRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        //UnityEngine.Debug.Log("Grounded: " + grounded + ", Double Jump: " + doubleJump + ", Floor Lasers: L " + floorLaserL + ", R " + floorLaserR + ", Input: " + input);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            playerRB.velocity = new Vector2(0, 0);
            gameActive = false;
        }
    }
}