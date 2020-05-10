using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractGrab : MonoBehaviour
{
    public bool isInside;

    //public string colliderTypeString = "none";

    public string nameOfThisObject;

    public Image inventoryImage;
    public Image inventory;

    InventoryHandler inventoryHandler;


    private void Awake()
    {
        if(PlayerPrefs.GetInt("hasAxe") == 1 || PlayerPrefs.GetInt("usedAxe") == 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //colliderTypeString = collision.GetType().ToString();
        transform.localScale = new Vector3((transform.localScale.x + (transform.localScale.x / 10)), (transform.localScale.y + (transform.localScale.y / 10)), transform.localScale.z);

        isInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        isInside = false;
        transform.localScale = new Vector3((transform.localScale.x - (transform.localScale.x / 10)), (transform.localScale.y - (transform.localScale.y / 10)), transform.localScale.z);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //colliderTypeString = collision.GetType().ToString();
    }

    private void Start()
    {
        inventoryHandler = inventory.GetComponent<InventoryHandler>();
    }

    private void Update()
    {

        GetComponent<CircleCollider2D>().enabled = PlayerPrefs.GetInt("inFuture") != 1;

        if (isInside)
        {
            if (Input.GetButtonDown("Jump"))
            {

                
                inventory.color = new Color(inventory.color.r, inventory.color.g, inventory.color.b, 1);
                inventoryImage.color = new Color(inventoryImage.color.r, inventoryImage.color.g, inventoryImage.color.b, 1);

                if (nameOfThisObject == "Axe")
                {
                    PlayerPrefs.SetInt("hasAxe", 1);
                }

                /*
                for (int i = 0; i < inventoryHandler.variables.Length; i++)
                {
                    if (inventoryHandler.variables[i].varName == nameOfThisObject)
                    {
                        inventoryHandler.variables[i].isInInventory = true;
                        PlayerPrefs.SetInt(inventoryHandler.variables[i].varName, (inventoryHandler.variables[i].isInInventory ? 1 : 0));

                    }
                }
                */

                inventoryImage.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                gameObject.SetActive(false);

            }

        }


    }

}