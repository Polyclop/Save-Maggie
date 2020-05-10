using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTrigger : MonoBehaviour
{

    public float fadingTime;
    float startTime, currentTime, deltaTime;
    float percentToAdd;
    public bool doTheFadeThing;
    public Image image;
    public bool isInside;

    public AudioSource endGameMusic;
    public float maxEndGameMusicDist;
    public AudioSource[] soundEffect;

    AudioSource sadSong;
    public bool didPlaySound;

    private void Start()
    {
        //soundEffect = GetComponent<AudioSource>();
        sadSong = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //colliderTypeString = collision.GetType().ToString();
        if (!didPlaySound)
        {
            sadSong.Play();
            didPlaySound = true;
        }

        isInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        isInside = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //colliderTypeString = collision.GetType().ToString();
    }


    private void Update()
    {
        if (!doTheFadeThing && isInside)
        {
            //Stuff
            if (Input.GetButtonDown("Jump"))
            {
                doTheFadeThing = true;
                startTime = Time.time;
            }


        }
        else if (doTheFadeThing) HandleFade();
    }

    void HandleFade()
    {
        currentTime = Time.time;
        deltaTime = currentTime - startTime;

        percentToAdd = Time.deltaTime / fadingTime;

        if (deltaTime <= fadingTime)
        {
            //Fade
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + percentToAdd);
            for(int i = 0; i< soundEffect.Length; i++)
            {
                soundEffect[i].volume -= percentToAdd * 2;
            }
            endGameMusic.maxDistance += percentToAdd * maxEndGameMusicDist;
            endGameMusic.volume += percentToAdd;
        }
        else
        {
            // After Fade
            if (Input.anyKeyDown)
            {
                Application.Quit();
            }
            
        }

    }
}
