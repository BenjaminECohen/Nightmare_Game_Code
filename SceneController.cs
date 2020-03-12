using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject fadeScreen;
    public GameObject title;
    // Start is called before the first frame update
    void Start()
    {
        title.GetComponent<Animator>().SetBool("FadeOut", true);
        fadeScreen.GetComponent<Animator>().SetBool("FadeOut", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void loadSceneWithFade(int sceneIndex)
    {
        fadeScreen.GetComponent<Animator>().SetBool("FadeOut", false);
        fadeScreen.GetComponent<Animator>().SetBool("FadeIn", true);
        StartCoroutine(loadSceneStagger(sceneIndex));
    }

    private IEnumerator loadSceneStagger(int sceneIndex)
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(sceneIndex);

    }

}
