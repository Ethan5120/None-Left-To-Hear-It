using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepSounds : MonoBehaviour
{
    public AudioSource walkFoot, runFoot;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                walkFoot.enabled = false;
                runFoot.enabled = true;
            }
            else
            {
                walkFoot.enabled = true;
                runFoot.enabled = false;
            }
        }
        else
        {
            walkFoot.enabled = false;
            runFoot.enabled = false;
        }
    }
}