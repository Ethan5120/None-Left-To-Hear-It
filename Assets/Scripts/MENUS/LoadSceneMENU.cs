using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMENU : MonoBehaviour
{
   public string Game;
   public void LoadNewSceneMENU()
   {
        SceneManager.LoadScene(Game);
   }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
