using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToTurnOff;
    [SerializeField] GameObject[] objectsToTurnOn;
    [SerializeField] Button start;


    public void ChangePannel()
    {
        foreach (GameObject objects in objectsToTurnOff)
        {
            objects.SetActive(false);
        }
        foreach (GameObject things in objectsToTurnOn)
        {
            things.SetActive(true);
        }
        start.Select();
    }
}

