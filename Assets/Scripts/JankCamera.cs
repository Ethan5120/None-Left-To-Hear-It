using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JankCamera : MonoBehaviour
{
    [SerializeField] GameObject Camera1;
    [SerializeField] GameObject Camera2;

    public void ChangeCamera()
    {
        if(Camera1.activeSelf)
        {
            Camera1.SetActive(false);
        }
        else
        {
            Camera1.SetActive(true);
        }
       
    }
}
