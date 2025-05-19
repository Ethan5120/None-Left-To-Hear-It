using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerData", menuName = "ManagerData")]
public class GM_ScriptableObject : ScriptableObject
{
    public float gameTime = 1f;
    public bool isHubDoorOpen;
    public bool isEnergyOn;
    
}
