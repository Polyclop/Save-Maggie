using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithObject : MonoBehaviour
{
    public bool isInside;

    //public string colliderTypeString = "none";

    public string objectNeededToActivate;
    public bool hasObject;

    public Image inventoryImage;
    public Image inventory;

    public Sprite newObject;
    public Sprite brokenObject;

    AudioSource soundEffect;
    public AudioClip clip1, clip2;
    bool firstStep, secondStep;

    public float fadingTime, timeBetweenInOut;
    float startTime, currentTime, deltaTime;
    float percentToAdd;
    bool doTheFadeThing;
    public bool goForward = true;
    bool startedPause, inPause, didPause;
    public RawImage image;

    public string textToShow = "LivingRoom";
    public Text textElement;
    public Image blackFont;

    public bool reading = false;

    public AudioSource ssound;
    public AudioClip readSound, happySong;

    public CapsuleCollider2D endGameTrigger;

    //InventoryHandler inventoryHandler;

    private void Awake()
    {
        firstStep = (PlayerPrefs.GetInt("seenFactory") == 1) ? true : false;
        if (objectNeededToActivate == "Axe")
        {
            hasObject = PlayerPrefs.GetInt("hasAxe") == 1 ? true : false;
        }
        secondStep = (PlayerPrefs.GetInt("usedAxe") == 1) ? true : false;
        GetComponent<CircleCollider2D>().enabled = !secondStep;
        if (firstStep)
        {
            GetComponent<SpriteRenderer>().sprite = secondStep ? brokenObject : newObject;
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
        //inventoryHandler = inventory.GetComponent<InventoryHandler>();
        soundEffect = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        GetComponent<CircleCollider2D>().enabled = PlayerPrefs.GetInt("inFuture") != 1;


        if (isInside && !doTheFadeThing)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (!firstStep)
                {
                    soundEffect.clip = clip1;
                    soundEffect.Play();
                    GetComponent<SpriteRenderer>().sprite = newObject;
                    firstStep = true;
                    PlayerPrefs.SetInt("seenFactory", 1);
                }
                else if(!secondStep)
                {
                    if (objectNeededToActivate == "Axe")
                    {
                        hasObject = PlayerPrefs.GetInt("hasAxe") == 1 ? true : false;
                    }
                    /*
                    for (int i=0; i<inventoryHandler.variables.Length; i++)
                    {
                        if (inventoryHandler.variables[i].varName == objectNeededToActivate)
                        {
                            hasObject = inventoryHandler.variables[i].isInInventory;
                            inventoryHandler.variables[i].isInInventory = false;
                            PlayerPrefs.SetInt(inventoryHandler.variables[i].varName, (inventoryHandler.variables[i].isInInventory ? 1 : 0));
                        }
                    }
                    */
                    if (hasObject)
                    {
                        doTheFadeThing = true;
                        startTime = Time.time;
                        

                    }
                }

            }

        }
        if (doTheFadeThing)
        {
            HandleFade();
        }
        if (reading)
        {
            doTheFadeThing = false;
            if (Input.anyKeyDown)
            {
                reading = false;
                textElement.text = "";
                textElement.gameObject.SetActive(false);
                blackFont.gameObject.SetActive(false);

                
            }
        }

    }

    void HandleFade()
    {
        currentTime = Time.time;
        deltaTime = currentTime - startTime;
        percentToAdd = Time.deltaTime / fadingTime * 2;
        if ((deltaTime <= fadingTime / 2) && !inPause)
        {
            image.color = goForward ? new Color(image.color.r, image.color.g, image.color.b, image.color.a + percentToAdd) : new Color(image.color.r, image.color.g, image.color.b, image.color.a - percentToAdd);
        }
        else if (!didPause)
        {
            if (!startedPause)
            {
                startedPause = true;
                inPause = true;
                startTime = Time.time;

                // Stuff
                soundEffect.clip = clip2;
                soundEffect.Play();

                gameObject.GetComponent<SpriteRenderer>().sprite = brokenObject;
                inventoryImage.sprite = null;

                inventory.color = new Color(inventory.color.r, inventory.color.g, inventory.color.b, 0);
                inventoryImage.color = new Color(inventoryImage.color.r, inventoryImage.color.g, inventoryImage.color.b, 0);

                secondStep = true;
                hasObject = false;

                if (objectNeededToActivate == "Axe")
                {
                    PlayerPrefs.SetInt("hasAxe", 0);
                    PlayerPrefs.SetInt("usedAxe", 1);

                }

            }
            if (deltaTime >= timeBetweenInOut)
            {
                startedPause = false;
                inPause = false;
                didPause = true;
                startTime = Time.time;
                goForward = false;
                soundEffect.clip = happySong;
                soundEffect.Play();
            }
        }
        else
        {
            goForward = true;
            doTheFadeThing = false;

            reading = true;
            ssound.clip = readSound;
            textElement.gameObject.SetActive(true);
            textElement.text = textToShow;
            blackFont.gameObject.SetActive(true);
            ssound.Play();
            endGameTrigger.enabled = true;

        }
        
        


    }
}
