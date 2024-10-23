using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMENU : MonoBehaviour
{   
   public void LoadNewSceneMENU()
    {
        SceneManager.LoadScene("BLOCKOUT");
    }
}
