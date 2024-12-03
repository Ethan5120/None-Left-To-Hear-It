using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyData enemyData;
    public EnemyData.EnemyState newState;
    private Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private float distance;
    [SerializeField] bool isAttacking;
    [SerializeField] float unstuckTime;

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

    void Start()
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
                enemyData.newPosition = enemyData.startingPosition;
                enemyData.newRotation = enemyData.startingRotation;
                enemyData.deadTime = 0;
                enemyData.firstLoad = false;     
            }
            else
            {
                transform.SetPositionAndRotation(enemyData.newPosition, enemyData.newRotation);
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
            case EnemyData.EnemyState.aliveState:
            {
                if(!VoicesSource.isPlaying)
                {
                    StartAudioHell();
                }
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
                    StartAttack();
                }
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
        enemyData.currentState = EnemyData.EnemyState.aliveState;
        enemyData.enemyHealth = 1;
        enemyData.deadTime = 0;
    }

    public void SaveData(Component sender, object data)
    {
        enemyData.newPosition = transform.position;
        enemyData.newRotation = transform.rotation;
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
