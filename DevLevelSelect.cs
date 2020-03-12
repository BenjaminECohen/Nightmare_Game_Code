using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevLevelSelect : MonoBehaviour
{
    public int level1Index;
    public int level2Index;
    public int level3Index;
    public int level4Index;
    private bool unlockSelect = false;
    public AudioSource audio;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (unlockSelect)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene(level1Index);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene(level2Index);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene(level3Index);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SceneManager.LoadScene(level4Index);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            audio.PlayOneShot(clip);
            unlockSelect = true;
        }
    }
}
