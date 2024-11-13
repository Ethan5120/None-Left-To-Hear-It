using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseSettings;
    public GameObject PauseVHS;
    public GameObject Inventory;

    public PlayerController player; 

    public bool paused;
    public bool inventaryOpen;

    public string menuSceneName;

    public PlayerSO keyData;

    public Image k0, k1, k2, k3, k4, k5;

    private void Start()
    {
       paused = false;
       inventaryOpen = false;

    }

    // Update is called once per frame
    void Update()
    {

        UpdateInventory();

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            paused = !paused;
            if(inventaryOpen == true)
            {
                paused  = false;
            }
            else
            {
                if(paused == true) 
                {
                    player.enabled = false;  
                    PauseMenu.SetActive(true);             
                    Time.timeScale = 0;
                    Inventory.SetActive(false);
                    //Cursor.visible = true;
                    //Cursor.lockState = CursorLockMode.None;
                    //AudioListener.pause = false;
                    PauseVHS.SetActive(true);
                }
                if(paused == false)
                {
                    player.enabled=true;
                    PauseMenu.SetActive(false);
                    Time.timeScale = 1;
                    Inventory.SetActive(false);
                    //Cursor.visible = false;
                    //Cursor.lockState = CursorLockMode.Locked;
                    //AudioListener.pause = false;
                    PauseSettings.SetActive(false);
                    PauseVHS.SetActive(false);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            inventaryOpen = !inventaryOpen;
            if(paused == true)
            {
                inventaryOpen  = false;
            }
            else
            {
                if(inventaryOpen == true) 
                {
                    player.enabled = false;  
                    PauseMenu.SetActive(false);             
                    Time.timeScale = 0;
                    Inventory.SetActive(true);
                    //Cursor.visible = true;
                    //Cursor.lockState = CursorLockMode.None;
                    //AudioListener.pause = false;
                 }
                 if(inventaryOpen == false)
                {
                    player.enabled=true;
                    PauseMenu.SetActive(false);
                    Time.timeScale = 1;
                    Inventory.SetActive(false);
                    //Cursor.visible = false;
                    //Cursor.lockState = CursorLockMode.Locked;
                    //AudioListener.pause = false;
                }
            }
        }
    }

    public void resumeGame()
    {
        player.enabled = true;
        PauseMenu.SetActive(false);
        Inventory.SetActive(false);
        paused = false;
        inventaryOpen = false;
        Time.timeScale=1;
        //Cursor.visible = false;
        //Cursor.lockState= CursorLockMode.Locked;
        //AudioListener.pause= false;
        PauseVHS.SetActive(false);
    }
    public void backToMenu()
    {
        Time.timeScale = 1;
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        //AudioListener.pause = false;
        SceneManager.LoadScene(menuSceneName);
        Debug.Log("Yendo A Menu");

    }
    public void quitGamePause()
    {
        Application.Quit();
        Debug.Log("Pause Quitting...");
    }

    public void UpdateInventory()
    {
        k0.enabled = keyData.PlayerKeys[0];
        k0.enabled = keyData.PlayerKeys[1];
        k0.enabled = keyData.PlayerKeys[2];
        k0.enabled = keyData.PlayerKeys[3];
        k0.enabled = keyData.PlayerKeys[4];
        k0.enabled = keyData.PlayerKeys[5];
    }
}
