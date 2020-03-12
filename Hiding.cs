using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding : MonoBehaviour
{
    private GameObject player;
    private GameObject hiddenPlayer;
    private bool isHiding = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Hiding"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isHiding && !player.GetComponent<Animator>().GetBool("IsCrying"))
            {
                //INSERT SOUND OF CHARACTER HIDING
                player.tag = "Hiding";
                player.GetComponent<Movement_2D_Player>().DisablePlayerMovement();
                player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.0f);
                isHiding = true;

            
            }
            else if (Input.GetKeyDown(KeyCode.E) && isHiding)
            {
                //INSERT SOUND OF CHARACTER HIDING
                player.tag = "Player";
                player.GetComponent<Movement_2D_Player>().EnablePlayerMovement();
                player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1.0f);
                isHiding = false;


            }
        }
    }
    
}
