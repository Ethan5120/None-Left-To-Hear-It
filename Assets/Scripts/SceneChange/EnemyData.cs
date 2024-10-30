using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SceneData/EnemyData")]
public class EnemyData : ScriptableObject
{
    public bool firstLoad = true;

    [Header("EnemyState")]
    public int enemyHealth;
    public float deadTime; 
    public bool startAmbush = false;
    public enum EnemyState{aliveState, deadState, ambushState}
    public EnemyState startingState;
    public EnemyState currentState;
    
    [Header("Original positions")]
    public Vector3 startingPosition;
    public Quaternion startingRotation;

    [Header("On Load Positions")]
    public Vector3 newPosition;
    public Quaternion newRotation;
}
