using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerItems : MonoBehaviour
{
    public PlayerController playerControllerScript;

    public GameObject snowballIcon;
    public GameObject snowball;
    public GameObject molotov;
    public GameObject[] snow;
    public TextMeshProUGUI snowballCountDisplay;
    public TextMeshProUGUI molotovCountDisplay;
    public TextMeshProUGUI stickCountDisplay;
    public TextMeshProUGUI metalCountDisplay;
    public TextMeshProUGUI bottleCountDisplay;
    public TextMeshProUGUI fabricCountDisplay;

    public bool snowballEnabled = true;
    public bool shovelEnabled = false;
    public bool molotovEnabled = false;

    public int snowballCount = 0;
    public int molotovCount = 0;
    public int stickCount = 0;
    public int metalCount = 0;
    public int bottleCount = 0;
    public int fabricCount = 0;

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
        molotovCountDisplay.text = molotovCount.ToString();
        if (molotovCount == 0)
        {
            molotovCountDisplay.color = Color.red;
        }
        else
        {
            molotovCountDisplay.color = Color.white;
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
        bottleCountDisplay.text = bottleCount.ToString();
        if (bottleCount == 0)
        {
            bottleCountDisplay.color = Color.red;
        }
        else
        {
            bottleCountDisplay.color = Color.white;
        }
        fabricCountDisplay.text = fabricCount.ToString();
        if (fabricCount == 0)
        {
            fabricCountDisplay.color = Color.red;
        }
        else
        {
            fabricCountDisplay.color = Color.white;
        }

        //Throw snowball
        if (UnityEngine.Input.GetKeyDown(KeyCode.Z) && snowballEnabled)
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
        if (UnityEngine.Input.GetKeyDown(KeyCode.X) && shovelEnabled && playerControllerScript.grounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 0.8f);
            for (int i = 0; i < snow.Length; i++)
            {
                if (hit.transform.gameObject == snow[i])
                {
                    snowballCount = 9;
                    snowballIcon.SetActive(true);
                    snowballEnabled = true;
                    Debug.Log("Snow refilled!");
                }
            }
        }

        //Throw molotov
        if (UnityEngine.Input.GetKeyDown(KeyCode.C) && molotovEnabled)
        {
            if (molotovCount > 0)
            {
                molotovCount--;
                Instantiate(molotov, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            }
            else
            {
                Debug.Log("No molotovs!");
            }
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
            case "Bottle":
                bottleCount++;
                break;
            case "Fabric":
                fabricCount++;
                break;
            default:
                break;
        }
        Destroy(collision.transform.root.gameObject);
    }
}
