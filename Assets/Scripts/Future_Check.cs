using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Future_Check : MonoBehaviour
{
    public Sprite newSprite;
    bool changeSprite;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("usedAxe") == 1) { 
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("usedAxe") == 1) { 
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}
