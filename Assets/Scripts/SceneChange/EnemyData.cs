using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SceneData/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int enemyHealth;
    public bool isAlive;
    public bool isAmbush;
    public float deadTime; 
    
    [Header("Original positions")]
    public Vector3 originalPosition;
    public Quaternion originalPotation;

    [Header("On Load Positions")]
    public Vector3 newPosition;
    public Quaternion newPotation;
}
