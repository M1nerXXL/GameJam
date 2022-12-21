using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class IgnoreBackgroundWalls : MonoBehaviour
{
    public Collider2D ownCollider;
    private GameObject[] backgroundWalls;

    // Start is called before the first frame update
    void Start()
    {
        //Look for background walls
        backgroundWalls = GameObject.FindGameObjectsWithTag("Background Wall"); ;
        //Ignore collision with all background walls
        for (int i = 0; i < backgroundWalls.Length; i++)
        {
            Physics2D.IgnoreCollision(ownCollider, backgroundWalls[i].GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
