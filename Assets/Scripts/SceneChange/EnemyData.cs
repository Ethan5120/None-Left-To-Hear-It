using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SceneData/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int enemyHealth;
    public bool isAlive;
    public float deadTime; 
    public Vector3 position;
    public Quaternion rotation;
}
