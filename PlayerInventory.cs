using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private GameObject gameManager;
    public AudioSource audio;
    public AudioClip clip;
    //public GameObject gameplayKey;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Controls picking up keys and displaying key acquisition on canvas
        if (collision.CompareTag("Key"))
        {
            collision.gameObject.SetActive(false);
            gameManager.GetComponent<MyGameController>().toggleKeyGot();
            audio.PlayOneShot(clip);

        }
        if (collision.CompareTag("Flashlight"))
        {
            collision.gameObject.SetActive(false);
            gameManager.GetComponent<MyGameController>().toggleFlashlight();
            this.GetComponent<Movement_2D_Player>().flashlight.flashlight.SetActive(gameManager.GetComponent<MyGameController>().playerHasFlashlight());
        }
    }

    
}
