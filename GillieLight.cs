using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GillieLight : MonoBehaviour
{
    //Turns the GillieLight on when it is active
    private Patroller parentActiveAtStart;
    private float originalIntensity;
    // Start is called before the first frame update
    void Start()
    {
        parentActiveAtStart = GetComponentInParent<Patroller>();
        originalIntensity = GetComponent<Light>().intensity;
        if (!parentActiveAtStart.active)
        {
            GetComponent<Light>().intensity = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parentActiveAtStart.active)
        {
            GetComponent<Light>().intensity = originalIntensity;
        }
        else
        {
            GetComponent<Light>().intensity = 0.0f;
        }
    }
}
