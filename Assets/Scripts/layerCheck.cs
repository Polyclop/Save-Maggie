using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerCheck : MonoBehaviour
{
    public GameObject maggie;

    SpriteRenderer maggieRenderer;
    public GameObject[] objects;
    SpriteRenderer[] renderers;
    int did = 0;

    public int maxMaggieOrder;

    public int currentMaggieOrder;

    // Start is called before the first frame update
    void Start()
    {
        renderers = new SpriteRenderer[objects.Length];
        for(int i=0; i<objects.Length; i++)
        {
            renderers[i] = objects[i].GetComponentInParent<SpriteRenderer>();
        }

        maggieRenderer = maggie.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        did = 0;
        for (int i = 0; i < objects.Length; i++)
        {
            if (maggie.transform.position.y > objects[i].transform.position.y)
            {
                did++;
                maggieRenderer.sortingOrder = renderers[i].sortingOrder - 1;
            }
        }
        if(did == 0)
        {
            maggieRenderer.sortingOrder = maxMaggieOrder;
        }

        currentMaggieOrder = maggieRenderer.sortingOrder;
    }
}
