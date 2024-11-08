using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PausaMenu;
    public GameObject PausaSettings;
    public GameObject PausaVHS;
    public GameObject Inventario;
    public PlayerController player; 
    public bool paused;
    //public bool pausedInv;
    public string menuSceneName;

    private void Start()
    {
       PausaMenu.SetActive(false);
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
                Time.timeScale = 1;
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
                //AudioListener.pause = false;
                PausaSettings.SetActive(false);
                PausaVHS.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            paused = !paused;
            if(paused == true) 
            {
                player.enabled = false;  
                Inventario.SetActive(true);             
                Time.timeScale = 0;
                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
                //AudioListener.pause = false;
            }
            if(paused == false)
            {
                player.enabled=true;
                Inventario.SetActive(false);
                Time.timeScale = 1;
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
                //AudioListener.pause = false;
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
