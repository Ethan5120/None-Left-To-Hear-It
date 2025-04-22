using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SceneData/EnemyData")]
public class EnemyData : ScriptableObject
{
    public bool firstLoad = true;
    public enum EnemyState{idleState, chaseState, deadState, ambushState}
    public EnemyState currentState;
    public float deadTime; 
}
