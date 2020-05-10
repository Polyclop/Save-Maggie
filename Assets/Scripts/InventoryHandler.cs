using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    /*
    public struct Vars
    {
        public string varName;
        public bool isInInventory;
    }

    public int numberOfObjects = 1;

    public Vars[] variables = new Vars[1];

    public string[] neededObjectNames = { "Axe" };
    */
    Color inventoryColor;

    bool canDo = true;


    public bool hasAxe;
    public Sprite axe;



    private void Awake()
    {
        /*for (int i = 0; i < variables.Length; i++)
        {
            if (PlayerPrefs.GetInt(variables[i].varName) != 0)
            {

                inventoryColor = gameObject.GetComponent<Image>().color;
                GetComponent<Image>().color = new Color(inventoryColor.r, inventoryColor.g, inventoryColor.b, 255);
                inventoryColor = GetComponentsInChildren<Image>()[1].color;
                GetComponentsInChildren<Image>()[1].color = new Color(inventoryColor.r, inventoryColor.g, inventoryColor.b, 255);
                canDo = false;
            }
        }
        */
        hasAxe = PlayerPrefs.GetInt("hasAxe") == 1 ? true : false;
        if (hasAxe)
        {
            inventoryColor = gameObject.GetComponent<Image>().color;
            GetComponent<Image>().color = new Color(inventoryColor.r, inventoryColor.g, inventoryColor.b, 255);
            inventoryColor = GetComponentsInChildren<Image>()[1].color;
            GetComponentsInChildren<Image>()[1].color = new Color(inventoryColor.r, inventoryColor.g, inventoryColor.b, 255);
            canDo = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (canDo)
        {
            inventoryColor = gameObject.GetComponent<Image>().color;
            GetComponent<Image>().color = new Color(inventoryColor.r, inventoryColor.g, inventoryColor.b, 0);
            inventoryColor = GetComponentsInChildren<Image>()[1].color;
            GetComponentsInChildren<Image>()[1].color = new Color(inventoryColor.r, inventoryColor.g, inventoryColor.b, 0);

            hasAxe = false;
            PlayerPrefs.SetInt("hasAxe", hasAxe? 1 : 0);

        }

        if (hasAxe)
        {
            GetComponentsInChildren<Image>()[1].sprite = axe;
        }
        /*variables = new Vars[numberOfObjects];
        for (int i=0; i< variables.Length; i++)
        {
            variables[i].varName = neededObjectNames[i];
            variables[i].isInInventory = false;
            PlayerPrefs.SetInt(variables[i].varName, (variables[i].isInInventory ? 1 : 0));
        }
        */


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
