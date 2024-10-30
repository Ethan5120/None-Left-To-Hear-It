using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PausaMenu, PausaControls, PausaSettings, PausaQuit, PausaVHS;
    public PlayerController player; 
    public bool paused;
    public string menuSceneName;

    private void Start()
    {
       PausaMenu.SetActive(false);
       PausaControls.SetActive(false);
       PausaSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            paused = !paused;
            if(paused == true) 
            {
                player.enabled = false;  
                PausaMenu.SetActive(true);
                PausaControls.SetActive(false);
                PausaSettings.SetActive(false);
                PausaQuit.SetActive(false);
                Time.timeScale = 0;
                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
                //AudioListener.pause = false;
                PausaVHS.SetActive(true);
            }
            if(paused == false)
            {
                player.enabled=true;
                PausaMenu.SetActive(false);
                PausaControls.SetActive(false);
                PausaSettings.SetActive(false);
                PausaQuit.SetActive(false);
                Time.timeScale = 1;
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
                //AudioListener.pause = false;
                PausaVHS.SetActive(false);
            }
        }
    }

    public void resumeGame()
    {
        player.enabled = true;
        PausaMenu.SetActive(false);
        Time.timeScale=1;
        //Cursor.visible = false;
        //Cursor.lockState= CursorLockMode.Locked;
        //AudioListener.pause= false;
        paused = false;
        PausaVHS.SetActive(false);
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
}
