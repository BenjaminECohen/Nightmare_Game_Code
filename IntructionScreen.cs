using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntructionScreen : MonoBehaviour
{
    public GameObject fadeScreen;
    private Animator fadeAnim;
    private bool allowInput = false;
    public AudioSource audio;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        fadeAnim = fadeScreen.GetComponent<Animator>();
        fadeAnim.SetBool("FadeOutNormal", true);
        StartCoroutine(HoldInput());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && allowInput)
        {
            allowInput = false;
            audio.PlayOneShot(audioClip);
            fadeAnim.SetBool("FadeOutNormal", false);
            fadeAnim.SetBool("FadeIn", true);
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(2);
    }

    private IEnumerator HoldInput()
    {
        yield return new WaitForSeconds(3.5f);
        allowInput = true;
    }
}
