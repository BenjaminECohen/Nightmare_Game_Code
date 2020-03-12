using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGameController : MonoBehaviour
{
    

    public static MyGameController GM;
    public GameObject pauseMenu; //Put in your pause menu game object
    //Have a gameObject placed as a SPAWN location and put that as the starting respawnPoint in the Inspector
    public Transform respawnPoint;
    //Make sure to assign your player in Inspector
    private GameObject player;
    private GameObject[] enemies;
    private GameObject[] key;
    public GameObject keyCanvas;
    public GameObject flashlightCanvas;
    private float originalSpeed;
    public bool hasFlashlight = false;
    private bool[] keyGot;
    private bool[] keyKept;
    private bool[] keyUsed;
    private bool menuIsOpen = false;


    private void Start()
    {
        //This finds the player if they have the Player tag
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        originalSpeed = player.GetComponent<Movement_2D_Player>().speed;
        key = GameObject.FindGameObjectsWithTag("Key");
        keyUsed = new bool[key.Length];
        keyGot = new bool[key.Length];
        keyKept = new bool[key.Length];
        for (int i = 0; i < key.Length; i++)
        {
            keyUsed[i] = false;
            keyGot[i] = false;
            keyKept[i] = false;
        }
        pauseMenu.SetActive(false);
        keyCanvas.SetActive(false);
        if (!hasFlashlight)
        {
            flashlightCanvas.SetActive(false);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !menuIsOpen)
        {
            pauseMenu.SetActive(true);
            menuIsOpen = true;

            player.GetComponent<Movement_2D_Player>().DisablePlayerMovement();
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Patroller>().enabled = false;
                enemies[i].GetComponent<Animator>().enabled = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuIsOpen)
        {
            pauseMenu.SetActive(false);
            menuIsOpen = false;
            player.GetComponent<Movement_2D_Player>().EnablePlayerMovement();
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Patroller>().enabled = true;
                enemies[i].GetComponent<Animator>().enabled = true;
            }
        }

        if (searchIfKeyGot())
        {
            keyCanvas.SetActive(true);
        }
        else
        {
            keyCanvas.SetActive(false);
        }

        if (hasFlashlight)
        {
            flashlightCanvas.SetActive(true);
        }
        else
        {
            flashlightCanvas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            devStats();
        }
    }

    public void PlayerKill()
    {
        //Debug.Log("Player Killed");
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        ResetKeys();
        //places player at last respawn point
        player.transform.position = respawnPoint.position;
        player.GetComponent<Movement_2D_Player>().speed = originalSpeed;
        //Debug.Log("Respawn Success");
    }

    public void toggleFlashlight()
    {
        if (hasFlashlight)
        {
            hasFlashlight = false;
            flashlightCanvas.SetActive(false);
        }
        else
        {
            hasFlashlight = true;
            flashlightCanvas.SetActive(true);
        }
    }
    
    public bool playerHasFlashlight()
    {
        if (hasFlashlight)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    /*Controls Key Collectables: Resets the keys if player did not reach
     * a checkpoint/respawn point before dying with the key
    */
    public void ResetKeys()
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (!keyKept[i])
            {

                if (!keyUsed[i]) //If the key has not been used, reset it
                {
                    if (!key[i].activeInHierarchy) //If the key is inactive, set it to be active
                    {
                        key[i].SetActive(true);
                        keyGot[i] = false;
                    }

                }


            }
        }
        
    }

    public void toggleKeyGot()
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (!key[i].activeInHierarchy && !keyUsed[i])
            {
                keyGot[i] = true;
            }
        }
    }

    public bool useKey()
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (keyGot[i])
            {
                keyUsed[i] = true;
                keyGot[i] = false;
                keyKept[i] = false;
                return true;
            }
        }
        return false;
    }

    public bool searchIfKeyGot()
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (keyGot[i])
            {
                return true;
            }
        }
        return false;
    }

    public void saveKeys()
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (keyGot[i])
            {
                keyKept[i] = true;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void devStats()
    {
        string keyString = "";
        string keyUsedString = "";
        string keyGotString = "";
        string keyKeptString = "";
        for (int i = 0; i < key.Length; i++)
        {

            keyString += "key " + (i + 1) + ": " + key[i].activeInHierarchy + ", ";
            keyGotString += "key " + (i + 1) + ": " + keyGot[i] + ", ";
            keyUsedString += "key " + (i + 1) + ": " + keyUsed[i] + ", ";
            keyKeptString += "key " + (i + 1) + ": " + keyKept[i] + ", ";

        }
        Debug.Log("Key Status");
        Debug.Log("Keys in map: " + keyString);
        Debug.Log("Keys got: " + keyGotString);
        Debug.Log("Keys kept: " + keyKeptString);
        Debug.Log("Keys used: " + keyUsedString);
    }

}
