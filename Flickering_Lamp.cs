using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering_Lamp : MonoBehaviour
{
    public Sprite lampOn;
    public Sprite lampOff;
    public float maxIntensity;
    public AudioClip lampOffSound;
    public AudioClip lampOnSound;
    private Light lampLight;
    private float timer = 0.0f;
    private float randomNum;
    private AudioSource audio;
    private bool coroutinePlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        lampLight = GetComponentInChildren<Light>();
        randomNum = Random.Range(0.2f, 1.0f);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= randomNum && !coroutinePlaying)
        {
            StartCoroutine(lightFlicker());


        }

    }

    private IEnumerator lightFlicker()
    {
        coroutinePlaying = true;
        GetComponent<SpriteRenderer>().sprite = lampOff;
        lampLight.intensity = 0.0f;
        audio.PlayOneShot(lampOffSound, 0.1f);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().sprite = lampOn;
        audio.PlayOneShot(lampOnSound, 0.1f);
        lampLight.intensity = maxIntensity;
        timer = 0.0f;
        randomNum = Random.Range(0.2f, 1.0f);
        coroutinePlaying = false;
    }
}
