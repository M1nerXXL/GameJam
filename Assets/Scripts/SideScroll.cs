using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll : MonoBehaviour
{
    public GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Scroll limit
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        //Scrolling
        if (gameObject.tag == "Background Far")
        {
            transform.position = new Vector3(0.8f * x, 0.8f * y - 1.5f, transform.position.z);
        }else if (gameObject.tag == "Background Close")
        {
            transform.position = new Vector3(0.5f * x, 0.5f * y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
