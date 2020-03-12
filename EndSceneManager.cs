using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject monster;
    public bool beginMonster = false;
    public int monsterWaitTime = 3;
    public float monsterFadeTime = 5.0f;
    public GameObject player;
    public GameObject lamp;
    private float timer = 0.0f;
    private float timer2 = 0.0f;
    private bool remainSleeping = false;
    private bool beginMonster2 = false;
    private bool lastEndMoments = false;
    public GameObject fadeScreen;
    private float exitSceneTime = 1.0f;
    public int nextScene = 0;

    void Start()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        monster.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (beginMonster)
        {
            GetComponentInChildren<ParticleSystem>().Play();
            player.GetComponent<Animator>().SetBool("IsSleeping", true);
            remainSleeping = true;
            StartCoroutine(MonsterWait());
            beginMonster = false;
        }
        if (beginMonster2 && timer <= monsterFadeTime)
        {                  
            timer += Time.deltaTime;
            FadeInMonster();           
        }
        else if (!remainSleeping)
        {
            player.GetComponent<Animator>().SetBool("IsSleeping", false);
        }
        if(timer >= monsterFadeTime)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= 2.0f)
            {
                fadeScreen.GetComponent<Animator>().SetBool("FadeIn", true);
            }
            if (timer2 >= 4.0f && exitSceneTime >= 0.0f)
            {
                monster.GetComponentInChildren<AudioSource>().volume = 1 - ((timer2 - 4.0f) / 2.0f);
                exitSceneTime = 1.0f - ((timer2 - 4.0f) / 2.0f);
            }
            if (exitSceneTime <= 0.0f)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    public void FadeInMonster()
    {
        monster.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, timer / monsterFadeTime);
        monster.GetComponentInChildren<AudioSource>().volume = timer / monsterFadeTime;        
    }

    private IEnumerator MonsterWait()
    {
        yield return new WaitForSeconds(2.0f);
        lamp.GetComponent<AudioSource>().Play();
        lamp.GetComponentInChildren<Light>().intensity = 0.0f;
        yield return new WaitForSeconds(monsterWaitTime);
        beginMonster2 = true;
    }
}
