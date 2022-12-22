using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    private PlayerController playerControllerScript;

    public Rigidbody2D molotovRB;
    private Collider2D playerCollider;
    public Collider2D molotovCollider;
    private GameObject player;
    public ParticleSystem molotovParticles;
    public ParticleSystem iceParticles;
    public ParticleSystem crateParticles1;
    public ParticleSystem crateParticles2;

    private Vector2 PositionOnHit;

    public float molotovSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Variables
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider2D>();
        playerControllerScript = player.GetComponent<PlayerController>();

        //Ignore player in collision
        Physics2D.IgnoreCollision(molotovCollider, playerCollider);
        //Move
        if (playerControllerScript.facingRight)
        {
            molotovRB.velocity = new Vector2(molotovSpeed, molotovSpeed);
        }
        else
        {
            molotovRB.velocity = new Vector2(-molotovSpeed, molotovSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy crate and snowball
        if (collision.gameObject.tag == "Ice")
        {
            PositionOnHit = collision.transform.position;          
            Instantiate(iceParticles, PositionOnHit, Quaternion.identity);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Crate")
        {
            PositionOnHit = collision.transform.position;
            Instantiate(crateParticles1, PositionOnHit, Quaternion.identity);
            Instantiate(crateParticles2, PositionOnHit, Quaternion.identity);
            collision.gameObject.SetActive(false);
        }
        Instantiate(molotovParticles, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
