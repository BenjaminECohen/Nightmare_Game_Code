using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latcher : MonoBehaviour
{
    public float sleepLength = 5.0f;    //The latcher will stay asleep for X seconds
    public float sleepIntervals = 5.0f; //The latcher will sleep every X seconds
    private float sleepCounter = 0.0f;
    private bool isSleeping = false;
    public float grappleRange = 4.0f;
    public float grappleTimeVariable = 0.2f;
    private GameObject player;
    private Vector3 playerColliderOffset;
    private RaycastHit2D detectionRay;
    //public bool playerFound = false;
    private bool grabbed = false;
    public GameObject latchSprite;
    private float currentStretchPos = 0.0f;
    private GameObject[] toggledTiles;

    private AudioSource laughter;
    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerColliderOffset = player.GetComponent<BoxCollider2D>().offset;
        latchSprite.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        toggledTiles = GameObject.FindGameObjectsWithTag("toggleOnGrab");
        laughter = GetComponent<AudioSource>();
        laughter.volume = 0.0f;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Casts the raycast to detect the player
        detectionRay = Physics2D.Linecast(transform.position, player.transform.position + playerColliderOffset);
        //Use this debug if it seems the linecast isnt working
        //Debug.DrawLine(transform.position, player.transform.position + playerColliderOffset);

        if (sleepCounter >= sleepIntervals && !isSleeping)
        {
            GetComponent<Animator>().SetBool("isSleeping", true);
            isSleeping = true;
            sleepCounter = 0.0f;
        }
        else if (isSleeping && sleepCounter >= sleepLength)
        {
            GetComponent<Animator>().SetBool("isSleeping", false);
            isSleeping = false;
            sleepCounter = 0.0f;
        }
        else
        {
            sleepCounter += Time.fixedDeltaTime;
        }
           
        if (!grabbed && !isSleeping && detectionRay.collider.tag == "Player")
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= grappleRange)
            {
                sleepCounter = 0.0f; //That monster aint sleeping soon!
                float distance = Mathf.Abs(Vector2.Distance(player.transform.position, transform.position));
                latchSprite.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                GetComponent<Animator>().SetBool("Grabbed", true);               
                if (currentStretchPos < 0.5f)
                {
                    //gTV decides the base time for grapple at max range, gR/dist is a multiplier based on how close the player is to the enemy
                    laughter.volume = 0.5f;
                    currentStretchPos += Time.fixedDeltaTime * grappleTimeVariable * (grappleRange /distance);
                    tongueStretch();
                }
                else //Pull the sprite in
                {
                    player.GetComponent<Movement_2D_Player>().DisablePlayerMovement();
                    grabbed = true;
                    for (int i = 0; i < toggledTiles.Length; i++)
                    {
                        toggledTiles[i].SetActive(false);
                    }
                }
            }
            else
            {
                laughter.volume = 0.0f;
                GetComponent<Animator>().SetBool("Grabbed", false);
                currentStretchPos -= Time.fixedDeltaTime * grappleTimeVariable;
                if (currentStretchPos > 0.0f)
                {;
                    tonguePullEmpty();
                }
                else
                {
                    currentStretchPos = 0.0f;
                }
             
            }
        }
        else if (grabbed)
        {
            currentStretchPos = 0.0f;
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 0.05f);
            tonguePullPlayer();
            //toggle grabbed if the player has died
            if (Vector2.Distance(transform.position, player.transform.position) > grappleRange)
            {
                for (int i = 0; i < toggledTiles.Length; i++)
                {
                    toggledTiles[i].SetActive(true);
                }
                latchSprite.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                GetComponent<Animator>().SetBool("Grabbed", false);
                player.GetComponent<Movement_2D_Player>().EnablePlayerMovement();
                grabbed = false;
            }
        }
        else //If the player collider is no longer detected it will continue retracting
        {
            laughter.volume = 0.0f;
            GetComponent<Animator>().SetBool("Grabbed", false);
            currentStretchPos -= Time.fixedDeltaTime * grappleTimeVariable;
            if (currentStretchPos > 0.0f)
            {
                tonguePullEmpty();
            }
            else
            {
                currentStretchPos = 0.0f;
            }

        }

        


        
        
    }

    //Stretch TOWARD the player Sprite
    private void tongueStretch()
    {
        Vector2 distanceToPlayer = player.transform.position - transform.position;
        //currentStretchPos will have a max of 0.5
        Vector2 centeredPos = Vector2.Lerp(transform.position, player.transform.position, currentStretchPos);
        latchSprite.transform.position = centeredPos;
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(distanceToPlayer.x, 2.0f) + Mathf.Pow(distanceToPlayer.y, 2.0f));
        latchSprite.transform.localScale = new Vector3(hypotenuse * 1.25f * currentStretchPos * 2.0f, 1.0f, 1.0f);

        //Finds angle between the y and x distance via tangent
        float angleToPlayer = Mathf.Atan2(distanceToPlayer.y, distanceToPlayer.x) * Mathf.Rad2Deg;
        //defines the rotation on the z axis. Vector3.forward is the z axis.
        Quaternion quatRotation = Quaternion.AngleAxis(angleToPlayer, Vector3.forward);
        latchSprite.transform.rotation = Quaternion.Slerp(latchSprite.transform.rotation, quatRotation, 40.0f * Time.fixedDeltaTime);
    }

    //Pull the player Sprite in if they are grappled
    private void tonguePullPlayer()
    {
        
        Vector2 distanceToPlayer = player.transform.position - transform.position;
        Vector2 centeredPos = Vector2.Lerp(transform.position, player.transform.position, 0.5f);
        latchSprite.transform.position = centeredPos;
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(distanceToPlayer.x, 2.0f) + Mathf.Pow(distanceToPlayer.y, 2.0f));
        latchSprite.transform.localScale = new Vector3(hypotenuse * 1.25f, 1.0f, 1.0f);
        
        //Finds angle between the y and x distance via tangent
        float angleToPlayer = Mathf.Atan2(distanceToPlayer.y, distanceToPlayer.x) * Mathf.Rad2Deg;
        //defines the rotation on the z axis. Vector3.forward is the z axis.
        Quaternion quatRotation = Quaternion.AngleAxis(angleToPlayer, Vector3.forward);
        latchSprite.transform.rotation = Quaternion.Slerp(latchSprite.transform.rotation, quatRotation, 40.0f * Time.fixedDeltaTime);
        


    }
    
    //Will pull back if the player has NOT been grappled
    private void tonguePullEmpty()
    {
        Vector2 distanceToPlayer = player.transform.position - transform.position;
        //currentStretchPos will have a max of 0.5
        Vector2 centeredPos = Vector2.Lerp(transform.position, player.transform.position, currentStretchPos);
        latchSprite.transform.position = centeredPos;
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(distanceToPlayer.x, 2.0f) + Mathf.Pow(distanceToPlayer.y, 2.0f));
        latchSprite.transform.localScale = new Vector3(hypotenuse * 1.25f * currentStretchPos * 2.0f, 1.0f, 1.0f);

    }

}
