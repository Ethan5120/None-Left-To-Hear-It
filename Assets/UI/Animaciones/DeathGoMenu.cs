using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathGoMenu : MonoBehaviour
{
    public string MenuName;
    public AudioSource ButtonAudio;

    void GoToMenu(string MenuName)
    {
        SceneManager.LoadScene(MenuName);
    }

    private void AudioPlay()
    {
        ButtonAudio.Play();
    }
}
