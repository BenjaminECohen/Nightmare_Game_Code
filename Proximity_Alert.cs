using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Proximity_Alert : MonoBehaviour
{
    // Start is called before the first frame update
    public float alertMinDist = 10;
    private GameObject[] enemies;
    private float closestDist = 100;
    private float distance;
    private GameObject player;
    public float shakeSpeed = 1.0f;
    private AudioSource audio;

    private bool temp = false;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
        audio.pitch = 1.0f;

    }

    // Update is called once per frame
    //FIXME: Need to make the proximity more refined AND ADD SOUND
    void Update()
    {


        /*Cycles through ALL enemies in the level and keeps only the closest distance 
         * to influence the proximity alert
        */
        for (int i = 0; i < enemies.Length; i++)
        {
            distance = Vector2.Distance(enemies[i].transform.position, player.transform.position);
            /*First enemy in the list is always registered initially as the closest distance
             * and is always updated if the level has only one enemy
             * */
            if (i == 0)
            {
                closestDist = distance;
            }
            /*Compares the rest of the monsters distances to the first and the lowest one of them all 
             * is the factor that determines the proximity alert alpha
             * */
            if (i > 0 && distance < closestDist && enemies.Length > 1)
            {
                closestDist = distance;
            }

        }
        /*Makes sure that the alert only shows when an enemy is within the range of 5 units 
         * and adjusts the alpha of the alert according to this in increments of 0.2
         * */
        //Debug.Log("Closest enemy is: " + closestDist + " meters away!");
        if (closestDist <= alertMinDist)
        {
            //Determines the alpha
            this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, (alertMinDist - closestDist) / alertMinDist);
            //Determines the rate at which the image shakes itself
            this.GetComponent<Image>().transform.position = new Vector3(Mathf.Sin(Time.time * shakeSpeed) + 
                this.transform.position.x, Mathf.Sin(Time.time * shakeSpeed) + this.transform.position.y, 
                this.transform.position.z);
        }
        else
        {
            this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        //Audio
        audio.volume = (alertMinDist - closestDist) / alertMinDist;



    }
}
