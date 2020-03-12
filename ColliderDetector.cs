using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerColliderOffset;
    private float detectionRange;
    private RaycastHit2D detection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerColliderOffset = player.GetComponent<BoxCollider2D>().offset;
        GetComponentInParent<Patroller>().raycastFoundPlayer = false;
        detectionRange = GetComponentInParent<Patroller>().chaseRange;


    }

    // Update is called once per frame
    void Update()
    {
        detection = Physics2D.Linecast(transform.position, player.transform.position + playerColliderOffset);
        //Use this debug if it seems the linecast isnt working
        //Debug.DrawLine(transform.position, player.transform.position + playerColliderOffset);

        if (detection.collider != null)
        {
            //If the linecast hits the player, then foundPlayer is activated
            if (detection.collider.tag == "Player")
            {
                GetComponentInParent<Patroller>().raycastFoundPlayer = true;
                //If the player is in chase distance, activate the character
                if (Vector2.Distance(transform.position, player.transform.position) <= detectionRange)
                {
                    GetComponentInParent<Patroller>().active = true;
                }
            }
            else
            {
                GetComponentInParent<Patroller>().raycastFoundPlayer = false;
            }
        }
        

    }

    
}
