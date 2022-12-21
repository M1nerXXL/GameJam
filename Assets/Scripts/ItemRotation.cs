using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    private float startY;
    public float rotationSpeed;
    public float floatingSpeed;
    public float floatingAmplitude;
    private float floatingTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y; 
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        //Float up and down
        floatingTime += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, startY + Mathf.Sin(floatingTime * floatingSpeed) * floatingAmplitude, transform.position.z);
    }
}
