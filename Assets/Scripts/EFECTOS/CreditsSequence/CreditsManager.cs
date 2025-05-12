using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] Animator LightAnimator;
    [SerializeField] string a_Flicker;


    [SerializeField] Animator TextAnimator;
    [SerializeField] string a_ScrollText;
    public void StartCredits()
    {
        TextAnimator.Play(a_ScrollText);
    }
    public void EndCredits()
    {
        LightAnimator.Play(a_Flicker);
    }
}
