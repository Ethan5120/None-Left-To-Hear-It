using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PickUpData", menuName = "SceneData/PickUpData")]
public class pickUp_SO : ScriptableObject
{
    public List<bool> itemActiveState;
}
