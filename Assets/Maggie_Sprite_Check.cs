using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maggie_Sprite_Check : MonoBehaviour
{
    SpriteRenderer maggieRend;
    public Sprite withGlasses, withoutGlasses;

    private void Awake()
    {
        maggieRend = GetComponent<SpriteRenderer>();
        maggieRend.sprite = (PlayerPrefs.GetInt("inFuture") == 1) ? withGlasses : withoutGlasses;
    }

}
