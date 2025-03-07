using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyData enemyData;
    public EnemyData.EnemyState newState;
    private Transform player;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform[] roamingTargets;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private float distance;
    [SerializeField] bool isAttacking;
    [SerializeField] bool chasingTarget;
    [SerializeField] float unstuckTime;
    [SerializeField] float detectionRange;
    [SerializeField] float interestRange;

    [Header("AnimationSettings")]
    [SerializeField] private Animator animator;
    [SerializeField] List<string> animations =  new List<string>();
    [Space(5)]

    [Header("AudioSettings")]
    public AudioClip FootstepSound;
    public AudioClip Deathsound;
    public AudioClip BiteSound;
    public AudioClip RiseSound;
    public AudioClip ArmSwingSound;
    public AudioClip RadioSound;

    public AudioSource FootstepSource;
    public AudioSource DeathSource;
    public AudioSource BiteSource;
    public AudioSource RiseSource;
    public AudioSource ArmSwingSource;
    public AudioSource VoicesSource;
    
    [Space(5)]

    [Header("AttackData")]
    private SphereCollider attackCollider;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if(enemyData != null)
        {
            if(enemyData.firstLoad)
            {
                enemyData.startAmbush = false;
                enemyData.currentState = enemyData.startingState;
                transform.SetPositionAndRotation(enemyData.startingPosition, enemyData.startingRotation);
                enemyData.enemyHealth = 1;
                enemyData.newRotation = enemyData.startingRotation;
                enemyData.deadTime = 0;
                enemyData.firstLoad = false;     
            }
            else
            {
                transform.SetPositionAndRotation(enemyData.newPositions[Random.Range(0,enemyData.newPositions.Length)], enemyData.newRotation);
            }
        }
        else
        {
            enemyData = ScriptableObject.CreateInstance<EnemyData>();
            enemyData.startingPosition = transform.position;
            enemyData.startingRotation = transform.rotation;
            enemyData.startingState = newState;
        }

        


        player = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();

        attackCollider = GetComponent<SphereCollider>();
        attackCollider.enabled = false;
    }


    void Update()
    {
        switch (enemyData.currentState)
        {
            case EnemyData.EnemyState.idleState:
            {
                if(!VoicesSource.isPlaying)
                {
                    StartAudioHell();
                }
                Roaming();    
                break;
            }

            case EnemyData.EnemyState.chaseState:
            {
                distance = Vector3.Distance(transform.position, player.position);

                if(animations != null && !isAttacking && distance > 2f)
                {
                    animator.Play(animations[0]);
                    agent.destination = player.position;
                    agent.isStopped = false;
                }

                if(distance <= 2f)
                {
                    Debug.Log("StartAttack");
                    //Aqui tenemos que checar si esta viendo al jugador NO LO OLVIDES
                    AttackCheck();
                }

                CheckPlayerDistance();
                break;
            }

            case EnemyData.EnemyState.deadState:
            {
                if(VoicesSource.isPlaying)
                {
                    StopAudioHell();
                }
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
                if(animations != null && !enemyData.startAmbush)
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

        if(isAttacking)
        {
            unstuckTime-= Time.deltaTime;
            if(unstuckTime <= 0)
            {
                isAttacking = false;
            }
        }
    }

#region IdleMethods
    void GetNewTarget()
    {
        chasingTarget = false;
        int currentTarget = 0;
        int newTarget;
        do
        {
        newTarget = Random.Range(0, roamingTargets.Length);
        }
        while(newTarget == currentTarget);
        currentTarget = newTarget;
        agent.destination = roamingTargets[currentTarget].position;
    }

    void Roaming()
    {
        agent.isStopped = false;
        if(agent.destination == null || chasingTarget)
        {
            GetNewTarget();
        }

        CheckPlayerDistance();
        distance = Vector3.Distance(transform.position, agent.destination);

        if(distance <= 2f)
        {
            GetNewTarget();
        }
    }

    void CheckPlayerDistance()
    {
        float pDistance = Vector3.Distance(transform.position, player.position);
        if(pDistance <= detectionRange && !chasingTarget)
        {
            chasingTarget = true;
            enemyData.currentState = EnemyData.EnemyState.chaseState;
        }

        if(pDistance >= interestRange && chasingTarget)
        {
            chasingTarget = false;
            enemyData.currentState = EnemyData.EnemyState.idleState;
        }
    }

#endregion



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

#region AttacksMethods
    void AttackCheck()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 10f, playerLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance);
            Debug.Log("PlayerIsInFront");
            StartAttack();
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance);
            transform.LookAt(player);
        }
    }

    public void StartAttack()
    {
        if(animations != null && !isAttacking)
        {
            animator.Play(animations[Random.Range(1, 3)]);
            isAttacking = true;
            unstuckTime = 3f;
        }
    }

    public void EndAttack()
    { 
        isAttacking = false;
    }
    public void Resurrect()
    {
        enemyData.currentState = EnemyData.EnemyState.idleState;
        enemyData.enemyHealth = 1;
        enemyData.deadTime = 0;
    }

    void EnableAttack()
    {
        attackCollider.enabled = true;
    }

    void DisableAttack()
    {
        attackCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
#endregion

    /////Animation Events (AUDIO)
#region AudioEvents
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

    void StartAudioHell()
    {
        VoicesSource.Play();
    }
    void StopAudioHell()
    {
        VoicesSource.Stop();
    }
#endregion
}
