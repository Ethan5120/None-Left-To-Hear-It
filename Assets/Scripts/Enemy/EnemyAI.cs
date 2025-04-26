using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Data")]
    public EnemyData enemyData;
    public GM_ScriptableObject managerData;

    [Header("EnemyState")]
    public int enemyMaxHealth = 1;
    public int enemyCurrentHealth = 1;
    public bool startAmbush = false;

    [Header("EnemySettings")]
    public EnemyData.EnemyState startingState;
    private Transform player;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool canRoam;
    [SerializeField] Transform[] roamingTargets;
    [SerializeField] Vector3 startLocation;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private float distance;
    [SerializeField] bool isAttacking;
    [SerializeField] bool chasingTarget;
    [SerializeField] float detectionRange;
    [SerializeField] float interestRange;


    [Header("EnemyTimers")]
    /// <summary>
    /// Failsafe en caso que el enemigo que quede atorado atacanda
    /// </summary>
    [SerializeField] float unstuckTime;
    /// <summary>
    /// Controla la frecuencia que el enemigo hara la animacion de idle en un tiempo minimo
    /// </summary>
    [SerializeField] float twitchFrecuencyMin;
    /// <summary>
    /// Controla el tiempo maximo que pasara para que el enemigo empieza a hacer twitch
    /// </summary>
    [SerializeField] float twitchFrecuencyMax;
    [SerializeField] float twitchTimer;
    [SerializeField] bool _twitching = false;


    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("AnimationNames")]
    [SerializeField] string standardAnimation;
    [Space(2)]
    [SerializeField] string walkAnimation;
    [Space(2)]
    [SerializeField] string deathAnimation;
    [Space(2)]
    [SerializeField] string riseAnimation;
    [Space(2)]
    [SerializeField] List<string> attackAnimations =  new List<string>();
    [SerializeField] List<string> hurtAnimations =  new List<string>();
    [SerializeField] List<string> idleAnimations =  new List<string>();
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
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float attackTimer;
    private SphereCollider attackCollider;

#region Initialitazion Methods
    void Awake()
    {
        animator = GetComponent<Animator>();
        if(enemyData != null)
        {
            if(enemyData.firstLoad)
            {
                startAmbush = false;
                enemyData.currentState = startingState;
                enemyCurrentHealth = enemyMaxHealth;
                enemyData.deadTime = 0;
                enemyData.firstLoad = false;     
            }
            else
            {
                SetStartLocation();
                if(enemyData.hasDied == true)
                {
                    enemyCurrentHealth = enemyMaxHealth / 2;
                }
            }
        }
        else
        {
            enemyData = ScriptableObject.CreateInstance<EnemyData>();
        }

        startLocation = gameObject.transform.position;


        player = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackCollider = GetComponent<SphereCollider>();
        attackCollider.enabled = false;
    }

    void SetStartLocation()
    {
        if(canRoam)
        {
            gameObject.transform.position = roamingTargets[Random.Range(0, roamingTargets.Length)].position;
        }
    }


#endregion
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
                
                Idleing();
                
                   
                break;
            }

            case EnemyData.EnemyState.chaseState:
            {
                distance = Vector3.Distance(transform.position, player.position);

                if(walkAnimation != null && !isAttacking && distance > 2f && managerData.gameTime > 0)
                {
                    animator.Play(walkAnimation);
                    agent.destination = player.position;
                    agent.isStopped = false;
                }

                if(distance <= 3f)
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
                if(deathAnimation != null && enemyData.deadTime >=0)
                {
                    animator.Play(deathAnimation);
                }
                agent.isStopped = true;
                enemyData.deadTime -= 1 * Time.deltaTime  * managerData.gameTime;
                if(enemyData.deadTime <= 0)
                {
                    if(riseAnimation != null)
                    {
                        animator.Play(riseAnimation);
                    }
                }
                break;
            }

            case EnemyData.EnemyState.ambushState:
            {
                if(deathAnimation != null && !startAmbush)
                {
                    animator.Play(deathAnimation);
                }
                agent.isStopped = true;
                if(startAmbush)
                {
                    if(riseAnimation != null)
                    {
                        animator.Play(riseAnimation);
                    }
                }
                break;
            }
        }

        if(isAttacking)
        {
            unstuckTime-= Time.deltaTime * managerData.gameTime;
            if(unstuckTime <= 0)
            {
                isAttacking = false;
            }
        }
    }

#region IdleMethods
/// <summary>
/// Selecciona el siguiente objetivo en la patrulla
/// </summary>
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

/// <summary>
/// Genera las acciones del idle
/// </summary>
    void Idleing()
    {
        CheckPlayerDistance(); 
        TwitchGenerator();
        if(canRoam && !_twitching && managerData.gameTime > 0)
        {
            agent.isStopped = false;
            animator.Play(walkAnimation);
            if(agent.destination == null || chasingTarget)
            {
                GetNewTarget();
            }   
            distance = Vector3.Distance(transform.position, agent.destination);

            if(distance <= 2f)
            {
                GetNewTarget();
            }
        }
        //aqui pon que se regrese a su lugar de spawn
        else if(!canRoam && !_twitching && managerData.gameTime > 0)
        {
            agent.destination = startLocation;
            distance = Vector3.Distance(transform.position, agent.destination);
            if(distance > 1)
            {
                agent.isStopped = false;
            }
            else
            {
                agent.isStopped = true;
                animator.Play(standardAnimation);
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void TwitchGenerator()
    {
        if(_twitching)
        {
            return;
        }

        twitchTimer -= Time.deltaTime * managerData.gameTime;
        if(twitchTimer <= 0)
        {
            if(idleAnimations != null)
            {
                _twitching = true;
                twitchTimer = Random.Range(twitchFrecuencyMin, twitchFrecuencyMax);
                animator.Play(idleAnimations[Random.Range(0, idleAnimations.Count)]);
            }
        }
    }

    public void StopTwitch()
    {
        _twitching = false;
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


/// <summary>
/// Ocaciona daño al enemigo y si no esta atacando es aturdido
/// </summary>
/// <param name="damageDealt"> La cantidad de daño que recibira el enemigo </param>
    public void TakeDamage(int damageDealt)
    {
        if(enemyData.deadTime <= 0)
        {
            enemyCurrentHealth -= damageDealt;
            if(hurtAnimations != null && !isAttacking)
            {
                animator.Play(hurtAnimations[Random.Range(0, hurtAnimations.Count)]);
            }

            if(enemyCurrentHealth <= 0)
            {
                Debug.Log("EnemyDead");
                enemyData.currentState = EnemyData.EnemyState.deadState;
                enemyData.hasDied = true;
                enemyData.deadTime = 30f;
            }
        }
    }

#region AttacksMethods
    void AttackCheck()
    {
        attackTimer -= 1 * Time.deltaTime * managerData.gameTime;
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(transform.position.x, 2.5f, transform.position.z), transform.forward, out hit, 5f, playerLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance);
            Debug.Log("PlayerIsInFront");
            StartAttack();
        }
        else if(!isAttacking)
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance);
            ///
            Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
            lookRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.05f);
        }
    }

    public void StartAttack()
    {
        if(attackAnimations != null && !isAttacking && attackTimer <= 0)
        {
            animator.Play(attackAnimations[Random.Range(0, attackAnimations.Count)]);
            isAttacking = true;
            unstuckTime = 3f;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
#endregion

#region AnimationEvents
    public void EndAttack()
    {
        attackTimer = attackCooldown; 
        isAttacking = false;
    }
    public void Resurrect()
    {
        enemyData.currentState = EnemyData.EnemyState.idleState;
        enemyCurrentHealth = enemyMaxHealth / 2;
        enemyData.deadTime = 0;
    }

    public void EnableAttack()
    {
        attackCollider.enabled = true;
    }

    public void DisableAttack()
    {
        attackCollider.enabled = false;
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

#region TestMethods
void OnDrawGizmosSelected() 
{
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(new Vector3(transform.position.x, 2.5f, transform.position.z), direction);
}


#endregion


}
