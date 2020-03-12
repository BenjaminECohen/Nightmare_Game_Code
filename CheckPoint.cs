using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour {
    // Attach this to your checkpoints. Checkpoints should have a collider 2D set to trigger.
    // If you want to make a sprite animate on activating the checkpoint, let me know! It shouldn't be too hard to program.
    private GameObject respawn;
    private bool activated = false;
    private Transform respawnPoint;
    private GameObject manager;
    public bool oneTimeCheckpoint = false;
    public Text checkpointReachText;
    public float checkpointTextLife = 2.0f;
    private float timer = 0.0f;
	
	void Start () {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        manager = GameObject.FindGameObjectWithTag("GameManager");
        checkpointReachText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
	}

    private void Update()
    {
        if (activated)
        {
            if (timer < checkpointTextLife)
            {
                timer += Time.deltaTime;
                checkpointReachText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f + ((checkpointTextLife - timer) / checkpointTextLife));
            }
            else
            {
                if(!oneTimeCheckpoint)
                {
                    timer = 0.0f;
                    activated = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated)
        {
            if (collision.CompareTag("Player"))
            {
                manager.GetComponent<MyGameController>().respawnPoint.position = transform.position;
                //Debug.Log("RESPAWN at: " + transform.position);
                if (manager.GetComponent<MyGameController>().searchIfKeyGot())
                {
                    //Debug.Log("With a KEY!!!");
                    manager.GetComponent<MyGameController>().saveKeys();
                }

                activated = true;


            }
        }

    }

            

}
