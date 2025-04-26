using System.Collections;
using System;
using UnityEngine;

public class Padlock_Rotate : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate { };
    private bool coroutineAllowed;
    [SerializeField] int numberShown;
    public PlayerController player;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        coroutineAllowed = true;
        transform.Rotate(0, 0, -36f * numberShown); //Rota la rueda a su posicion correxpondiente
        Rotated(name, numberShown); //Indica el resultado al control 
    }
    private void OnMouseDown()
    {
        if(coroutineAllowed && player.isInteracting)
        {
            StartCoroutine("RotateDial");
        }
    }

    private IEnumerator RotateDial()
    {
        coroutineAllowed = false;

        for(int i = 0; i <= 11; i++)
        {
            transform.Rotate(0, 0, -3f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        numberShown +=1;

        if(numberShown > 5)
        {
            numberShown = 0;
        }

        Rotated(name, numberShown);
    }
}
