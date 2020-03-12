using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    /*-----------------------------------------------------------
     * Settings that define the behavior patterns of the enemy!
     * ----------------------------------------------------------
     * */
    private bool chase = false;
    //If this enemy is not supposed to move immediately when the scene starts, set to false in editor
    public bool active = true;
    public bool activeOnRespawn = true;
    public float speed = 0.3f;
    public float chaseSpeed = 0.45f;
    //Determine the amount of Transform coordinates the enemy will patrol: 1-4
    public int patrolPoints = 1;
    public float chaseRange = 2.5f;

    /*---------------------------------------------
     * Coordinate settings
     * --------------------------------------------
     * */
    public Transform spawnPoint;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    public bool toPointB = true;
    public bool toPointC = false;
    public bool toPointD = false;
    public bool toPointB_R = true;
    public bool toPointC_R = false;
    public bool toPointD_R = false;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;
    private Vector3 pointCPosition;
    private Vector3 pointDPosition;
    private GameObject player;


    /*-------------------------------
     * Visual Settings
     * ------------------------------
     * */
    public bool flipXToA = false;
    public bool flipXToB = false;
    public bool flipXToC = false;
    public bool flipXToD = false;

    /*--------------------------------
     * Raycast Settings
     * -------------------------------
     * */
     //If the collider detector script is attached as a child, this is auto set to false in the detector script
    public bool raycastFoundPlayer = true;


    // Use this for initialization
    void Start()
    {
        pointAPosition = new Vector3(pointA.position.x, pointA.position.y, pointA.position.z);
        pointBPosition = new Vector3(pointB.position.x, pointB.position.y, pointB.position.z);
        pointCPosition = new Vector3(pointC.position.x, pointC.position.y, pointC.position.z);
        pointDPosition = new Vector3(pointD.position.x, pointD.position.y, pointD.position.z);
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 thisPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (toPointB && !chase && active && patrolPoints >= 2)
        {
            if (flipXToB)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed);
            if (thisPosition.Equals(pointBPosition))
            {
                toPointB = false;
                if (patrolPoints >= 3)
                {
                    toPointC = true;
                }
            }
        }
        else if (toPointC && !chase && active)
        {
            if (flipXToC)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, pointC.position, speed);
            if (thisPosition.Equals(pointCPosition))
            {
                toPointC = false;
                if (patrolPoints >= 4)
                {
                    toPointD = true;
                }
            }
        }
        else if (toPointD && !chase && active)
        {
            if (flipXToD)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, pointD.position, speed);
            if (thisPosition.Equals(pointDPosition))
            {
                toPointD = false;
            }
        }
        else
        {
            if (flipXToA)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (!chase && active)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed);
                if (thisPosition.Equals(pointAPosition))
                {
                    toPointB = true;
                }
            }

        }
        if (player.tag == "Player" && Vector3.Distance(transform.position, player.transform.position) <= chaseRange 
            && raycastFoundPlayer && active)
        {
            chase = true;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chaseSpeed);
        }
        else
        {
            chase = false;
        }

    }
    /*If the enemy touches the player, the enemy will reset to a specific spawn position.
     * This position DOES NOT have to be where they originally were placed in the editor
     * and you can appropriately change the bool values for patrol as needed for the spawn
     * point to work as intended.
     * */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.position = spawnPoint.transform.position;           
            toPointB = toPointB_R;
            toPointC = toPointC_R;
            toPointD = toPointD_R;
            active = activeOnRespawn;
        }
    }
}
