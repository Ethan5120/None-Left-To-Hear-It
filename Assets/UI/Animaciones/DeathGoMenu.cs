using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathGoMenu : MonoBehaviour
{
    public string MenuName;
    void GoToMenu(string MenuName)
    {
        SceneManager.LoadScene(MenuName);
    }
}
