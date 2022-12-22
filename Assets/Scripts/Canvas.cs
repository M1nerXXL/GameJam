using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public PlayerItems playerItemsScript;

    public GameObject[] turnOffAtStart;
    public GameObject craftingMenu;
    public GameObject shovelIcon;
    public GameObject molotovIcon;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < turnOffAtStart.Length; i++)
        {
            turnOffAtStart[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            craftingMenuToggle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && craftingMenu.active)
        {
            craftShovel();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && craftingMenu.active)
        {
            craftMolotov();
        }
    }

    public void craftingMenuToggle()
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
            shovelIcon.SetActive(true);
        }
        else
        {
            Debug.Log("Couldn't craft!");
        }
    }

    public void craftMolotov()
    {
        if (playerItemsScript.bottleCount >= 1 && playerItemsScript.fabricCount >= 1)
        {
            playerItemsScript.bottleCount--;
            playerItemsScript.fabricCount--;
            playerItemsScript.molotovCount++;
            playerItemsScript.molotovEnabled = true;
            molotovIcon.SetActive(true);
        }
        else
        {
            Debug.Log("Couldn't craft!");
        }
    }
}
