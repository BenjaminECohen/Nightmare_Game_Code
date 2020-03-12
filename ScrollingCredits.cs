using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollingCredits : MonoBehaviour
{

    public GameObject creditText;
    public Text skipText;
    public float creditSpeed = 40;
    public bool endOfCredits = false;
    public float volumeDecreaseTime = 3.0f;
    public int nextScene = 0;
    private float totalAfterCreditFinishTime;
    private float timer = 0.0f;
    private float skipTextTimer = 0.0f;
    private float startVolume;
    // Start is called before the first frame update
    void Start()
    {
        totalAfterCreditFinishTime = volumeDecreaseTime + 2.0f;
        startVolume = GetComponent<AudioSource>().volume;
        skipText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            creditText.transform.position = new Vector2(creditText.transform.position.x, creditText.transform.position.y + (creditSpeed * 3.0f) * Time.deltaTime);
        }
        else
        {
            creditText.transform.position = new Vector2(creditText.transform.position.x, creditText.transform.position.y + creditSpeed * Time.deltaTime);
        }
        
        if (endOfCredits)
        {
            timer += Time.deltaTime;
            if (timer <= volumeDecreaseTime)
            {
                GetComponent<AudioSource>().volume = startVolume - ((timer / volumeDecreaseTime) * startVolume);
            }
            if (timer >= totalAfterCreditFinishTime)
            {
                SceneManager.LoadScene(nextScene);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(nextScene);
        }

        skipTextTimer += Time.deltaTime;
        if (skipTextTimer >= 5.0f && skipTextTimer <= 6.0f)
        {
            skipText.color = new Color(1.0f, 1.0f, 1.0f, (skipTextTimer - 5.0f));

            
        }
    }
}
