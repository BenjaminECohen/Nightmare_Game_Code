using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator playerAnim;
    void Start()
    {
        playerAnim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnim.GetBool("WalkingRight"))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (playerAnim.GetBool("WalkingLeft"))
        {
            transform.rotation = Quaternion.Euler(180, 90, 0);
        }
        else if (playerAnim.GetBool("WalkingUp"))
        {
            transform.rotation = Quaternion.Euler(-90, 90, 0);
        }
        else if (playerAnim.GetBool("WalkingDown"))
        {
            transform.rotation = Quaternion.Euler(90, 90, 0);
        }
    }
}
