using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    public EnemyData.EnemyState newState;
    private Transform player;
    private NavMeshAgent agent;
    [SerializeField] private float distance;
    bool isAttacking;

    [Header("AnimationSettings")]
    [SerializeField] private Animator animator;
    [SerializeField] List<string> animations =  new List<string>();

    ////////////////////////AUDIO

    public AudioClip FootstepSound, Deathsound, BiteSound, RiseSound, ArmSwingSound;
    public AudioSource FootstepSource, DeathSource, BiteSource, RiseSource, ArmSwingSource;
    

    void Awake()
    {
        animator = GetComponent<Animator>();
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


    void Update()
    {
        switch (enemyData.currentState)
        {
            case EnemyData.EnemyState.aliveState:
            {
                if(animations != null && !isAttacking)
                {
                    animator.Play(animations[0]);
                    agent.destination = player.position;
                    agent.isStopped = false;
                }
               

                distance = Vector3.Distance(transform.position, player.position);
                if(distance <= 1f)
                {
                    Debug.Log("StartAttack");
                    StartAttack();
                }
                break;
            }

            case EnemyData.EnemyState.deadState:
            {
                if(animations != null && enemyData.deadTime >=0)
                {
                    animator.Play(animations[3]);
                }
                agent.isStopped = true;
                enemyData.deadTime -= 1 * Time.deltaTime;
                if(enemyData.deadTime <= 0)
                {
                    if(animations != null)
                    {
                        animator.Play(animations[4]);
                    }
                }
                break;
            }

            case EnemyData.EnemyState.ambushState:
            {
                if(animations != null)
                {
                    animator.Play(animations[3]);
                }
                agent.isStopped = true;
                if(enemyData.startAmbush)
                {
                    if(animations != null)
                    {
                        animator.Play(animations[4]);
                    }
                }
                break;
            }
        }
    }

    public void TakeDamage()
    {
        if(enemyData.deadTime <= 0)
        {
            enemyData.enemyHealth = 0;
            if(enemyData.enemyHealth <= 0)
            {
                Debug.Log("DamageTaken");
                enemyData.currentState = EnemyData.EnemyState.deadState;
                enemyData.deadTime = 30f;
            }
        }
    }

    public void StartAttack()
    {
        if(animations != null && !isAttacking)
        {
            animator.Play(animations[Random.Range(1, 3)]);
            agent.isStopped = true;
            isAttacking = true;
        }
    }

    public void EndAttack()
    {
        agent.isStopped = false;
        isAttacking = false;
    }
    public void Resurrect()
    {
        enemyData.currentState = EnemyData.EnemyState.aliveState;
        enemyData.enemyHealth = 1;
    }

    public void SaveData(Component sender, object data)
    {
        enemyData.newPosition = transform.position;
        enemyData.newRotation = transform.rotation;
    }

    /////Animation Events (AUDIO)
    
    public void PlayFootstepSounds()
    {
        FootstepSource.PlayOneShot(FootstepSound);
    }

    public void PlayDeathSound()
    {
        FootstepSource.PlayOneShot(Deathsound);
    }

    public void PlayAttackBite()
    {
        BiteSource.PlayOneShot(BiteSound);
    }

    public void PlayRise()
    {
        RiseSource.PlayOneShot(RiseSound);
    }

    public void PlayArmAttack()
    {
        ArmSwingSource.PlayOneShot(ArmSwingSound);
    }
}
