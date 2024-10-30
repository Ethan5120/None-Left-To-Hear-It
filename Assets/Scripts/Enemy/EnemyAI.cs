using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    public EnemyData.EnemyState newState;
    private Transform player;
    private NavMeshAgent agent;
    private float distance;
    


    void Awake()
    {
        if(enemyData == null)
        {
            enemyData = ScriptableObject.CreateInstance<EnemyData>();
            enemyData.startingPosition = transform.position;
            enemyData.startingRotation = transform.rotation;
            enemyData.startingState = newState;
        }

        if(enemyData.firstLoad)
        {
            enemyData.currentState = enemyData.startingState;
            transform.SetPositionAndRotation(enemyData.startingPosition, enemyData.startingRotation);
            enemyData.enemyHealth = 1;
            enemyData.firstLoad = false;
        }
        else
        {
            transform.SetPositionAndRotation(enemyData.newPosition, enemyData.newRotation);
        }
    }
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyData.currentState)
        {
            case EnemyData.EnemyState.aliveState:
            {
                agent.destination = player.position;
                agent.isStopped = false;

                distance = Vector3.Distance(agent.destination, player.position);
                if(distance <= 1)
                {
                    Debug.Log("StartAttack");
                }
                break;
            }

            case EnemyData.EnemyState.deadState:
            {
                agent.isStopped = true;
                enemyData.deadTime -= 1 * Time.deltaTime;
                if(enemyData.deadTime <= 0)
                {
                    enemyData.currentState = EnemyData.EnemyState.aliveState;
                }
                break;
            }

            case EnemyData.EnemyState.ambushState:
            {
                agent.isStopped = true;
                if(enemyData.startAmbush)
                {
                    enemyData.currentState = EnemyData.EnemyState.aliveState;
                }
                break;
            }
        }
    }

    public void TakeDamage()
    {
        enemyData.enemyHealth = 0;
        if(enemyData.enemyHealth <= 0)
        {
            Debug.Log("DamageTaken");
            enemyData.currentState = EnemyData.EnemyState.deadState;
            enemyData.deadTime = 30f;
        }
    }

    public void SaveData(Component sender, object data)
    {
        enemyData.newPosition = transform.position;
        enemyData.newRotation = transform.rotation;
    }
}
