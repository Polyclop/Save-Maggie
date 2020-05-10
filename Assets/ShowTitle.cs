using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTitle : MonoBehaviour
{
    
    AudioSource soundEffect;

    public float fadingTime;
    public float startTime;
    float currentTime, deltaTime;
    float percentToAdd;
    public bool doTheFadeThing;
    public bool goForward = true;
    bool startedPause, inPause, didPause;
    public Image rend;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("showedKeyboardTuto") == 0)
        {
            doTheFadeThing = true;
            startTime = Time.time;
        }
    }

    private void Start()
    {
        //soundEffect = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        /*if (!doTheFadeThing && PlayerPrefs.GetInt("showedKeyboardTuto") != 1) 
        {

                doTheFadeThing = true;
                startTime = Time.time;

        }*/
        if (doTheFadeThing) {
 
            HandleFade();
        }
    }

    void HandleFade()
    {
        currentTime = Time.time;
        deltaTime = currentTime - startTime;

        percentToAdd = Time.deltaTime / fadingTime * 2;

        if ((deltaTime <= fadingTime / 2) && !inPause)
        {
            //Fade
            rend.color = goForward ? new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a + percentToAdd) : new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a - percentToAdd);
        }
        else if (!didPause)
        {
            if (!startedPause)
            {
                startedPause = true;
                inPause = true;

                // Stuff Between Fades

                //soundEffect.Play();

                
            }
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                // Start Fade Out
                startedPause = false;
                inPause = false;
                didPause = true;
                startTime = Time.time;
                goForward = false;
            }
        }
        else
        {
            // After Fade
            goForward = true;
            doTheFadeThing = false;
            PlayerPrefs.SetInt("showedKeyboardTuto", 1);
        }

    }
}
