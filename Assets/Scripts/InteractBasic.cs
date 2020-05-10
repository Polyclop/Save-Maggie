using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBasic : MonoBehaviour
{

    public bool isInside;

    //public string colliderTypeString = "none";

    public Sprite newSprite;
    AudioSource soundEffect;
    bool didItAlready;

    private void Awake()
    {
        didItAlready = (PlayerPrefs.GetInt("seenZoo") == 1) ? true : false;
        GetComponent<CircleCollider2D>().enabled = !didItAlready;
        if (didItAlready)
        {
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        
    }

    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();
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

    private void Update()
    {
        GetComponent<CircleCollider2D>().enabled = PlayerPrefs.GetInt("inFuture") != 1;

        if (isInside && !didItAlready)
        {
            if (Input.GetButtonDown("Jump"))
            {
                soundEffect.Play();
                GetComponent<SpriteRenderer>().sprite = newSprite;
                transform.localScale = new Vector3((transform.localScale.x - (transform.localScale.x / 10)), (transform.localScale.y - (transform.localScale.y / 10)), transform.localScale.z);
                GetComponent<CircleCollider2D>().enabled = false;
                didItAlready = true;
                PlayerPrefs.SetInt("seenZoo", 1);
            }

        }


    }

}