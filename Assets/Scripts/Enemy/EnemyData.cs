using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SceneData/EnemyData")]
public class EnemyData : ScriptableObject
{
    public bool firstLoad = true;

    [Header("EnemyState")]
    public int enemyHealth = 1;
    public float deadTime; 
    public bool startAmbush = false;
    public enum EnemyState{idleState, chaseState, deadState, ambushState}
    public EnemyState startingState = EnemyState.idleState;
    public EnemyState currentState;

    [Header("Original positions")]
    public Vector3 startingPosition;
    public Quaternion startingRotation;

    [Header("On Load Positions")]
    public Vector3[] newPositions;
    public Quaternion newRotation;
}
