using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour
{
    //This script is meant to be attached to the player
    private GameObject[] enemies;
    private float closestDist = 100;
    private float distance;
    private float originalSpeed;
    public float freezeDist = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        originalSpeed = GetComponent<Movement_2D_Player>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            distance = Vector2.Distance(enemies[0].transform.position, transform.position);
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
        if (closestDist <= freezeDist)
        {
            GetComponent<Movement_2D_Player>().speed = 0.0f;
            GetComponent<Animator>().SetBool("IsCrying", true);
        }
        else
        {
            /*Player speed is reset on respawn. This can be changed if the player
             * finds some way to escape the enemy while in the frozen state.
             */
            GetComponent<Animator>().SetBool("IsCrying", false);
        }
    }
}
