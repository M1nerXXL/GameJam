using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerItems : MonoBehaviour
{
    public PlayerController playerControllerScript;

    public GameObject snowball;
    public TextMeshProUGUI snowballCountDisplay;
    public TextMeshProUGUI stickCountDisplay;
    public TextMeshProUGUI metalCountDisplay;

    public bool shovelEnabled = false;

    public int snowballCount = 0;
    public int stickCount = 0;
    public int metalCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Display number of items
        snowballCountDisplay.text = snowballCount.ToString();
        if (snowballCount == 0)
        {
            snowballCountDisplay.color = Color.red;
        }
        else
        {
            snowballCountDisplay.color = Color.white;
        }
        stickCountDisplay.text = stickCount.ToString();
        if (stickCount == 0)
        {
            stickCountDisplay.color = Color.red;
        }
        else
        {
            stickCountDisplay.color = Color.white;
        }
        metalCountDisplay.text = metalCount.ToString();
        if (metalCount == 0)
        {
            metalCountDisplay.color = Color.red;
        }
        else
        {
            metalCountDisplay.color = Color.white;
        }


        //Throw snowball
        if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
        {
            if (snowballCount > 0)
            {
                snowballCount--;
                Instantiate(snowball, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            }
            else
            {
                Debug.Log("No snowballs!");
            }
        }

        //Dig for snowballs
        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        {
            if (shovelEnabled && playerControllerScript.grounded)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 0.323f, transform.position.y), Vector2.down);
                if (hit.transform.tag == "Snow")
                {
                    snowballCount = 9;
                    Debug.Log("Snowballs refilled!");
                }
                else
                {
                    Debug.Log("Stand on snow first!");
                }
            }
        }

        //Item limit
        if (snowballCount > 9)
        {
            snowballCount = 9;
        }
        if (stickCount > 9)
        {
            stickCount = 9;
        }
        if (metalCount > 9)
        {
            metalCount = 9;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Add item and destroy
        switch (collision.gameObject.tag)
        {
            case "Stick":
                stickCount++;
                break;
            case "Metal":
                metalCount++;
                break;
            default:
                break;
        }
        Destroy(collision.transform.root.gameObject);
    }
}
