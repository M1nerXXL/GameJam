using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public PlayerItems playerItemsScript;

    public GameObject craftingMenu;
    public GameObject shovelIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerItemsScript.shovelEnabled)
        {
            shovelIcon.SetActive(true);
        }
    }

    public void craftingMenuButton()
    {
        if (craftingMenu.active)
        {
            craftingMenu.SetActive(false);
        }
        else
        {
            craftingMenu.SetActive(true);
        }
    }

    public void craftShovel()
    {
        if (playerItemsScript.stickCount >= 1 && playerItemsScript.metalCount >= 1)
        {
            playerItemsScript.stickCount--;
            playerItemsScript.metalCount--;
            playerItemsScript.shovelEnabled = true;
        }
        else
        {
            Debug.Log("Couldn't craft!");
        }
    }
}
