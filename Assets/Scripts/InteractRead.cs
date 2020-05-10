using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractRead : MonoBehaviour
{

    public bool isInside;

    //public string colliderTypeString = "none";
    
    public string textToShow = "LivingRoom";
    public Text textElement;
    public Image blackFont;

    public bool reading = false;

    public AudioSource ssound;
    public AudioClip readSound;



    public float red = 255;
    public float green = 255;
    public float blue = 255;
    public float alpha = 255;

    public Image image;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //colliderTypeString = collision.GetType().ToString();
        transform.localScale = new Vector3((transform.localScale.x + (transform.localScale.x / 10)), (transform.localScale.y + (transform.localScale.y / 10)), transform.localScale.z);
        GetComponent<SpriteRenderer>().color = new Color(red, green, blue, alpha);
        isInside = true;
        if(PlayerPrefs.GetInt("showedObjectInteractTuto") != 1)
        {
            image.color = new Color(1, 1, 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        isInside = false;
        transform.localScale = new Vector3((transform.localScale.x - (transform.localScale.x / 10)), (transform.localScale.y - (transform.localScale.y / 10)), transform.localScale.z);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        image.color = new Color(1, 1, 1, 0);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //colliderTypeString = collision.GetType().ToString();
    }

    private void Update()
    {
        GetComponent<CircleCollider2D>().enabled = PlayerPrefs.GetInt("inFuture") != 1;

        if (isInside)
        {
            if (Input.GetButton("Jump"))
            {
                reading = true;
                ssound.clip = readSound;
                textElement.gameObject.SetActive(true);
                textElement.text = textToShow;
                blackFont.gameObject.SetActive(true);
                ssound.Play();

                PlayerPrefs.SetInt("showedObjectInteractTuto", 1);
                image.color = new Color(1, 1, 1, 0);

            }
            if (reading)
            {
                if (Input.anyKeyDown)
                {
                    reading = false;
                    textElement.gameObject.SetActive(false);
                    blackFont.gameObject.SetActive(false);

                    textElement.text = "";
                }
            }
        }

    }

}