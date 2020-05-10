using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InteractDoor : MonoBehaviour
{

    public bool isInside;

    //public string colliderTypeString = "none";

    public string doorTo = "LivingRoom";

    public Image image;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //colliderTypeString = collision.GetType().ToString();
        isInside = true;
        if (PlayerPrefs.GetInt("showedDoorInteractTuto") != 1)
        {
            image.color = new Color(1, 1, 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //colliderTypeString = "none";
        isInside = false;
        image.color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //colliderTypeString = collision.GetType().ToString();
        isInside = true;
        if (Input.GetButton("Jump"))
        {
            image.color = new Color(1, 1, 1, 0);
            PlayerPrefs.SetInt("showedDoorInteractTuto", 1);

            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, doorTo));
        }

    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("usedAxe") == 1)
        {
            GetComponent<TilemapCollider2D>().enabled = false;
        }
    }

}
