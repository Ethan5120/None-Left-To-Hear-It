using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] Animator LightAnimator;
    [SerializeField] Animator LightAnimator_2;
    [SerializeField] string a_Flicker;

    [SerializeField] Animator ScreenAnimator;
    [SerializeField] string a_ScreenFlicker;
    [SerializeField] string a_ScreenOn;
    [SerializeField] string a_ScreenOff;



    [SerializeField] Animator TextAnimator;
    [SerializeField] string a_ScrollText;
    [SerializeField] string a_TextFlicker;



    [SerializeField] SceneField MainMenu;

    public void StartCredits()
    {
        TextAnimator.Play(a_ScrollText);
        ScreenAnimator.Play(a_ScreenOn);
    }
    public void EndCredits()
    {
        LightAnimator.Play(a_Flicker);
        LightAnimator_2.Play(a_Flicker);
        TextAnimator.Play(a_TextFlicker);
        ScreenAnimator.Play(a_ScreenFlicker);
    }
    public void ChangeToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    void Update()
    {
        if(Input.anyKey && TextAnimator.GetCurrentAnimatorStateInfo(0).IsName(a_ScrollText))
        {
            Debug.Log("PressingKey");
            TextAnimator.speed = 7.5f;
        }
        else
        {
            TextAnimator.speed = 1;
        }
    }
}
