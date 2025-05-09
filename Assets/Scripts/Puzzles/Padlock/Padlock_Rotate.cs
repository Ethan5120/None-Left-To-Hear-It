using System.Collections;
using System;
using UnityEngine;

public class Padlock_Rotate : MonoBehaviour
{
    public static event Action<GameObject, int> Rotated = delegate { };
    private bool coroutineAllowed;
    public int numberShown;
    
    private void Start()
    {
        coroutineAllowed = true;
        transform.Rotate(0, 0, -36f * numberShown); //Rota la rueda a su posicion correxpondiente
        Rotated(gameObject, numberShown); //Indica el resultado al control 
    }
    public void RotateDial(bool Increasing)
    {
        if(coroutineAllowed && Increasing)
        {
            StartCoroutine("RotateUpDial");
        }
        else
        {
            StartCoroutine("RotateDownDial");
        }
    }


    private IEnumerator RotateUpDial()
    {
        coroutineAllowed = false;
        Debug.Log("Rotating");


        for(int i = 0; i <= 11; i++)
        {
            transform.Rotate(0, 0, -3f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        numberShown +=1;

        if(numberShown > 9)
        {
            numberShown = 0;
        }

        Rotated(gameObject, numberShown);
    }

    private IEnumerator RotateDownDial()
    {
        coroutineAllowed = false;
        Debug.Log("Rotating");


        for(int i = 0; i <= 11; i++)
        {
            transform.Rotate(0, 0, 3f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        numberShown -=1;

        if(numberShown < 0)
        {
            numberShown = 9;
        }

        Rotated(gameObject, numberShown);
    }
}
