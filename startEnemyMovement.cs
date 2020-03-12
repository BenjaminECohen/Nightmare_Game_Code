using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startEnemyMovement : MonoBehaviour
{
    private GameObject[] enemies;
    private bool deactivate = false;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !deactivate)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Patroller>().active = true;
            }
            deactivate = true;
        }
    }
}
