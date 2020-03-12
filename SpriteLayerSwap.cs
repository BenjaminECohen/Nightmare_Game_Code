using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerSwap : MonoBehaviour
{
    public string frontalSortingLayer = "Foreground";
    public string backwardSortingLayer = "BackgroundCover";
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > player.transform.position.y)
        {
            //Set sorting layer to layer BEHIND the player
            GetComponent<SpriteRenderer>().sortingLayerName = frontalSortingLayer;
            GetComponentInChildren<SpriteRenderer>().sortingLayerName = frontalSortingLayer;
        }
        else
        {
            //Set sorting layer to layer INFRONT of the player
            GetComponent<SpriteRenderer>().sortingLayerName = backwardSortingLayer;
            GetComponentInChildren<SpriteRenderer>().sortingLayerName = backwardSortingLayer;
        }
    }
}
