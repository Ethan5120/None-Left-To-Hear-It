using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMENU : MonoBehaviour
{
   public SceneField sceneToLoad;

   public void LoadNewSceneMENU()
   {
        SceneManager.LoadScene(sceneToLoad);
   }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
