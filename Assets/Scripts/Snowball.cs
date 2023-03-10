using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Snowball : MonoBehaviour
{
    private PlayerController playerControllerScript;

    public Rigidbody2D snowballRB;
    private Collider2D playerCollider;
    public Collider2D snowballCollider;
    public SpriteRenderer snowballSprite;
    private GameObject player;
    public ParticleSystem snowballParticles;
    public ParticleSystem crateParticles1;
    public ParticleSystem crateParticles2;

    private Vector2 PositionOnHit;

    public float snowballSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Variables
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider2D>();
        playerControllerScript = player.GetComponent<PlayerController>();

        //Ignore player in collision
        Physics2D.IgnoreCollision(snowballCollider, playerCollider);
        //Move
        if (playerControllerScript.facingRight)
        {
            snowballRB.velocity = new Vector2(snowballSpeed, snowballSpeed / 5);
        }
        else
        {
            snowballRB.velocity = new Vector2(-snowballSpeed, snowballSpeed / 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy crate and snowball
        if (collision.gameObject.tag == "Crate")
        {
            PositionOnHit = collision.transform.position;
            Instantiate(crateParticles1, PositionOnHit, Quaternion.identity);
            Instantiate(crateParticles2, PositionOnHit, Quaternion.identity);
            collision.gameObject.SetActive(false);
        }
        Instantiate(snowballParticles, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
