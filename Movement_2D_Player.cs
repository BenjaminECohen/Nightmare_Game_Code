using UnityEngine;
using System.Collections;



public class Movement_2D_Player : MonoBehaviour
{

    public float speed;
    private float originalSpeed;
    private bool stopMovementInput = false;
    private Animator animator;

    [System.Serializable]
    public class FlashLight
    {
        public GameObject flashlight = null;
        public float intensity = 4;
        public bool onAtStart = true;
    }
    public FlashLight flashlight;
    

    private void Start()
    {
        originalSpeed = speed;
        animator = GetComponent<Animator>();
        flashlight.flashlight.SetActive(flashlight.onAtStart);
    }

    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, yMin, yMax;
    }

    public Boundary boundary;


    void FixedUpdate()
    {

        //CONTROLS

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        GetComponent<Rigidbody2D>().velocity = movement * speed;

        GetComponent<Rigidbody2D>().position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),
                0.0f
                );

        //ANIMATIONS
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        if (movement != new Vector3(0, 0, 0))
        {
            animator.SetFloat("lastHorizontal", movement.x);
            animator.SetFloat("lastVertical", movement.y);
        }

        if (flashlight.flashlight != null)
        {
            flashlight.flashlight.GetComponent<Animator>().SetFloat("Horizontal", movement.x);
            flashlight.flashlight.GetComponent<Animator>().SetFloat("Vertical", movement.y);
            flashlight.flashlight.GetComponent<Animator>().SetFloat("Magnitude", movement.magnitude);

            if (movement != new Vector3(0, 0, 0))
            {
                flashlight.flashlight.GetComponent<Animator>().SetFloat("lastHorizontal", movement.x);
                flashlight.flashlight.GetComponent<Animator>().SetFloat("lastVertical", movement.y);
            }
        }
    }

    public void DisablePlayerMovement()
    {
        speed = 0.0f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        GetComponent<Animator>().enabled = false;
    }

    public void EnablePlayerMovement()
    {
        speed = originalSpeed;
        GetComponent<Animator>().enabled = true;
    }

    void flashLightDirection(string argument, bool boolean)
    {
        if (flashlight.flashlight != null)
        {
            flashlight.flashlight.GetComponent<Animator>().SetBool(argument, boolean);
        }
    }
}