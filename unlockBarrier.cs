using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockBarrier : MonoBehaviour
{
    private GameObject gm;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager");
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && gm.GetComponent<MyGameController>().searchIfKeyGot())
        {
            this.GetComponent<DialogueTrigger>().denyDialogue = true;
            this.gameObject.SetActive(false);
            audio.Play();
            gm.GetComponent<MyGameController>().useKey();
            
        }

    }
}
