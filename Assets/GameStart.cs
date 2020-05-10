using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


public class GameStart : MonoBehaviour
{

    VideoPlayer video;
    public PlayerMove maggyMove;
    public SceneFader sceneFader;
    public ShowTitle keyboardTuto;
    public RawImage img;
    public AudioSource audioSo;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        if (PlayerPrefs.GetInt("playedIntro") != 1)
        {
            maggyMove.canMove = false;
            video.Play();
            audioSo.volume = 0;
            img.color = new Color(1, 1, 1, 1);
        }
        else
        {
            img.color = new Color(1, 1, 1, 0);
        }
    }

    private void Update()
    {
        video.loopPointReached += StartGame;
    }


    void StartGame(VideoPlayer vp)
    {
        sceneFader.enabled = true;
        keyboardTuto.gameObject.SetActive(true);
        maggyMove.canMove = true;
        audioSo.volume = 1;
        PlayerPrefs.SetInt("playedIntro", 1);
        img.color = new Color(1, 1, 1, 0);
    }
}
