using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public Rigidbody2D snowballRB;

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
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.74f), Vector2.left * 0.35f, Color.blue);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * 0.35f, Color.blue);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.74f), Vector2.left * 0.35f, Color.blue);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x - 0.35f, transform.position.y + 0.74f), Vector2.left * 0.05f, Color.red);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x - 0.35f, transform.position.y), Vector2.left * 0.05f, Color.red);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x - 0.35f, transform.position.y - 0.74f), Vector2.left * 0.05f, Color.red);
        bool wallLaserShortTL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.74f), Vector2.left, 0.35f);
        bool wallLaserShortML = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 0.35f);
        bool wallLaserShortBL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.74f), Vector2.left, 0.35f);
        bool wallLaserLongTL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.74f), Vector2.left, 0.4f);
        bool wallLaserLongML = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 0.4f);
        bool wallLaserLongBL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.74f), Vector2.left, 0.4f);

        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.74f), Vector2.right * 0.35f, Color.blue);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * 0.35f, Color.blue);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.74f), Vector2.right * 0.35f, Color.blue);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x + 0.35f, transform.position.y + 0.74f), Vector2.right * 0.05f, Color.red);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x + 0.35f, transform.position.y), Vector2.right * 0.05f, Color.red);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x + 0.35f, transform.position.y - 0.74f), Vector2.right * 0.05f, Color.red);
        bool wallLaserShortTR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.74f), Vector2.right, 0.35f);
        bool wallLaserShortMR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 0.35f);
        bool wallLaserShortBR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.74f), Vector2.right, 0.35f);
        bool wallLaserLongTR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.74f), Vector2.right, 0.4f);
        bool wallLaserLongMR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 0.4f);
        bool wallLaserLongBR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.74f), Vector2.right, 0.4f);

        //If wall on left, block left movement
        if (wallLaserLongTL || wallLaserLongML || wallLaserLongBL)
        {
            if (input < 0)
            {
                input = 0;
            }
            if (wallLaserShortTL || wallLaserShortML || wallLaserShortBL)
            {
                transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
            }
        }
        //If wall on right, block right movement
        if (wallLaserLongTR || wallLaserLongMR || wallLaserLongBR)
        {
            if (input > 0)
            {
                input = 0;
            }
            if (wallLaserShortTR || wallLaserShortMR || wallLaserShortBR)
            {
                transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y);
            }
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
        bool floorLaserL;
        bool floorLaserR;
        
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x - 0.275f, transform.position.y - 0.7f), Vector2.down * 0.1f, Color.red);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x - 0.275f, transform.position.y), Vector2.down * 0.7f, Color.blue);
        bool floorLaserLongL = Physics2D.Raycast(new Vector2(transform.position.x - 0.275f, transform.position.y), Vector2.down, 0.8f);
        bool floorLaserShortL = Physics2D.Raycast(new Vector2(transform.position.x - 0.275f, transform.position.y), Vector2.down, 0.7f);
        if (floorLaserLongL && !floorLaserShortL)
        {
            floorLaserL = true;
        }
        else
        {
            floorLaserL = false;
        }

        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x + 0.275f, transform.position.y - 0.7f), Vector2.down * 0.1f, Color.red);
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x + 0.275f, transform.position.y), Vector2.down * 0.7f, Color.blue);
        bool floorLaserLongR = Physics2D.Raycast(new Vector2(transform.position.x + 0.275f, transform.position.y), Vector2.down, 0.8f);
        bool floorLaserShortR = Physics2D.Raycast(new Vector2(transform.position.x + 0.275f, transform.position.y), Vector2.down, 0.7f);
        if (floorLaserLongR && !floorLaserShortR)
        {
            floorLaserR = true;
        }
        else
        {
            floorLaserR = false;
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
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
            playerRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        //UnityEngine.Debug.Log("Grounded: " + grounded + ", Double Jump: " + doubleJump + ", Floor Lasers: L " + floorLaserL + ", R " + floorLaserR + ", Input: " + input);
    }
}