using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMENU : MonoBehaviour
{
   public SceneField sceneToLoad;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = false;
        Time.timeScale = 1f;
    }

    public void LoadNewSceneMENU()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadByText(string text)
    {
        SceneManager.LoadScene(text);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
