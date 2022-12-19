using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;

    private bool doubleJump = false;
    public float walkingSpeed;
    public float jumpPower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Moving left and right
        float input = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * input * walkingSpeed);
        //Jumping
        bool grounded = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);
        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red);
        if (Input.GetKeyDown(KeyCode.Space) && (grounded || doubleJump))
        {
            if (grounded)
            {
                doubleJump = true;
            }
            else
            {
                doubleJump = false;
                playerRB.velocity = (new Vector2(playerRB.velocity.x, 0));
            }
            playerRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        Debug.Log(grounded + " " + doubleJump);
    }
}
