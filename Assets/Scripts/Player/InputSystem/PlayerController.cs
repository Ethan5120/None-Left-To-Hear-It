using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("InputSystemDeclarations")]
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    [SerializeField] public InputActionReference Move, Aim, Shoot, Interact, Pause, Inventory, Run;
    [Space(5)]

    [Header("GameManagerVariables")]
    public GM_ScriptableObject managerData;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] string cameraObjectName;
    [SerializeField] PlayerSO playerData;
    [Space(5)]

    [Header("PlayerVariables")]
    [SerializeField] SceneField DeathScene;
    [SerializeField] float playerWalkSpeed = 1;
    [SerializeField] float playerRunSpeed = 3;
    [SerializeField] float playerRotation = 40;
    [SerializeField] float playerAimSpeed = 20;
    [Space(5)]

    [Header("ShootingSettings")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] bool isShooting;
    [SerializeField] float unstuckTime; //En caso que se atore disparando.
    [Space(5)]

    [Header ("InteractSettings")]
    [SerializeField] float interactRange;
    [SerializeField] LayerMask interactLayer;
    [Space(5)]

    [Header("PlayerStatus")]
    [SerializeField] bool isAim = false; //Checa si el jugador esta apuntando
    [SerializeField] bool isAimingAnim = false;
    [SerializeField] float isRun = 0; //Checa si el jugador esta apuntando
    [SerializeField] public bool isInteracting = false; //Checa si el jugador esta interactuando
    [SerializeField] bool isTakingDamage = false; //Checa si el jugador esta recibiendo daño
    [Space(5)]

    [Header("AnimationData")]
    [SerializeField] Animator pAnimator;
    [SerializeField] string a_Idle;
    [SerializeField] string a_Walk;
    [SerializeField] string a_Run;
    [SerializeField] string a_Interact;

    [SerializeField] string a_Hurt;
    [SerializeField] string a_Death;
    //Animations With Guns
    [SerializeField] List<string> a_w_Idle =  new List<string>();
    [SerializeField] List<string> a_w_Aim =  new List<string>(); 
    [SerializeField] List<string> a_w_Attack =  new List<string>();
    [SerializeField] List<string> a_w_NoAmmo =  new List<string>();
    [SerializeField] List<string> a_w_Hurt =  new List<string>();
    [SerializeField] GameObject Gun;
    [SerializeField] Animator gunAnimator;
    [SerializeField] List<string> gunAnims =  new List<string>();
    [Space(5)]

    [Header("GroundChecks")]
    [SerializeField] bool isGrounded = true;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckDistance = 2;
    [Space(5)]

    ////Audio////
    [Header("AudioClips")]

    public AudioClip FootstepsSound, CockingSound, PickUpSound, DeathPSound, ShotAudio, DamageSound;
    public AudioSource FootstepsSource, CockingSource, PickUpSource, DeathPSource, ShotSource, DamageSource;

    ////////////
    

    private void OnEnable()
    {
        Aim.action.performed += EnterThirdPerson;
        Shoot.action.performed += PlayerShoot;
        Interact.action.performed += InteractCall;

        cameraManager = GameObject.Find(cameraObjectName).GetComponent<CameraManager>();
        pAnimator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        Aim.action.performed -= EnterThirdPerson;
        Shoot.action.performed -= PlayerShoot;
        Interact.action.performed -= InteractCall;
    }

    void Start()
    {
        ActivateGun();
    }


    private void EnterThirdPerson(InputAction.CallbackContext context)
    {
        if(!isAim && !isInteracting && !isTakingDamage)
        {
            cameraManager.TriggerThirdPerson();
            isAim = true;
            if(playerData.hasGun)
            {
                pAnimator.Play(a_w_Aim[0]);
                gunAnimator.Play(gunAnims[1]);
                isAimingAnim = true;
                spawnPoint.SetActive(true);
            }
        }
        else if(!isShooting && !isTakingDamage && !isInteracting)
        {
            cameraManager.TriggerThirdPerson();
            spawnPoint.SetActive(false);
            isAim = false;
        }
    }

    private void PlayerShoot(InputAction.CallbackContext context)
    {
        if(playerData.hasGun)
        {
            if(isAim && !isShooting && playerData.playerAmmo > 0 && !isTakingDamage && !isAimingAnim)
            {
                pAnimator.Play(a_w_Attack[0]);
                gunAnimator.Play(gunAnims[3]);
                var bull = Instantiate(bullet, spawnPoint.transform.position, transform.rotation);
                bull.GetComponent<bulletScript>().bulletLife = 5f;
                isShooting = true;
                playerData.playerAmmo--;
                unstuckTime = 0.5f;
            }
            else if (isAim && !isShooting && !isTakingDamage && !isAimingAnim)
            {
                pAnimator.Play(a_w_NoAmmo[0]);
                gunAnimator.Play(gunAnims[3]);
                isShooting = true;
                unstuckTime = 0.5f;
            }
        }
        
    }

    private void InteractCall(InputAction.CallbackContext context)
    {
        Ray r = new Ray (transform.position, transform.forward);
        if(Physics.Raycast(r, out RaycastHit hitInfo, interactRange, interactLayer))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out I_Interactable interactObject) && !isAim && !isTakingDamage)
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out playerPickUps pickUps))
                {
                    isInteracting = true;
                    pAnimator.Play(a_Interact);
                }
                interactObject.Interact();
            }
        }
    }

#region PlayerMovement

    //Handle Player Movement
    private void PlayerMovement()
    {
        //Check if Player is Grounded and apply gravity
        if(!isGrounded)
        {
            controller.Move(new Vector3(0, -1, 0));
        }
        else
        {
            controller.Move(new Vector3(0, 0, 0));
        }


        //Use the move method to move the player to the front and back
        if(!isAim)
        {
            if(playerData.hasGun){gunAnimator.Play(gunAnims[0]);}
            if(playerInput.y == 0 && playerInput.x != 0 && !isInteracting && !isTakingDamage)
            {
                pAnimator.Play(a_Walk);
            }

            if(playerInput.y > 0 && !isInteracting && !isTakingDamage)
            {   
                if(isRun == 0)
                {
                    controller.Move(transform.forward * playerInput.y * playerWalkSpeed * Time.deltaTime * managerData.gameTime);
                    pAnimator.Play(a_Walk);
                }
                else
                {
                    controller.Move(transform.forward * playerInput.y * playerRunSpeed * Time.deltaTime * managerData.gameTime);
                    pAnimator.Play(a_Run);
                }
            }
            else if(playerInput.y < 0 && !isInteracting && !isTakingDamage)
            {
                controller.Move(transform.forward * playerInput.y * playerWalkSpeed * Time.deltaTime * managerData.gameTime);
                pAnimator.Play(a_Walk);
            }


            if(playerInput.y == 0 && playerInput.x == 0 && !isAim && !isInteracting && !isTakingDamage)
            {
                pAnimator.Play(a_Idle);
            }
            
            

            transform.Rotate(transform.up, playerRotation * playerInput.x * Time.deltaTime * managerData.gameTime);
        }
        else
        {
            if(isAim && !isShooting && !isTakingDamage)
            {
                if(playerData.hasGun && !isAimingAnim)
                {
                    pAnimator.Play(a_w_Idle[0]);
                    gunAnimator.Play(gunAnims[2]);
                }
                else if(!playerData.hasGun)
                {
                    pAnimator.Play(a_Idle);
                }
                
            }
            //transform.Rotate(transform.right, playerAimSpeed * -playerInput.y * Time.deltaTime); //<----Encender a su propio riesgo
            transform.Rotate(transform.up, playerAimSpeed * playerInput.x * Time.deltaTime * managerData.gameTime);
        }
       
        
    }

    //Check if player is Grounded
    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
#endregion
    
    void Update()
    {
        if(managerData.gameTime > 0)
        {
            playerInput = Move.action.ReadValue<Vector2>();
            isRun = Run.action.ReadValue<float>();
            CheckGround();
            PlayerMovement();
        }
        if(unstuckTime >= 0)
        {
            unstuckTime -= 1 * Time.deltaTime * managerData.gameTime;
            if(unstuckTime <= 0)
            {
                isShooting = false;
            }
        }
    }



    public void TakeDamage()
    {
        isTakingDamage = true;
        playerData.playerHP--;
        if(playerData.playerHP > 0)
        {
            if(!isAim)
            {
                pAnimator.Play(a_Hurt);
            }
            else
            {
                pAnimator.Play(a_w_Hurt[0]);
            }
        }
        else
        {
            pAnimator.Play(a_Death);
        }
    }

    public void Heal()
    {
        if(playerData.playerPills > 0 && playerData.playerHP < 3)
        {
            playerData.playerPills--;
            playerData.playerHP++;
        }
    }

    public void DeathAnimEvent()
    {
        SceneManager.LoadScene(DeathScene);
    }


    void ActivateGun()
    {
        Gun.gameObject.SetActive(playerData.hasGun);
    }

#region AnimationEvents
//The following events are used in animation events.
    void Reload()
    {
        isShooting = false;
    }

    void StopInteract()
    {
        isInteracting = false;
    }

    void StopDamage()
    {
        isTakingDamage = false;
    }

    void StopAiming()
    {
        isAimingAnim = false;
    }
#endregion
 
   





#region AudioControl
//The following events are used to call the SFXs.
    public void PlayFootstepsAudio()
    {
        FootstepsSource.PlayOneShot(FootstepsSound);
    }

    public void PlayCockingAudio()
    {
        CockingSource.PlayOneShot(CockingSound);
    }

    public void PlayPickupAudio()
    {
        PickUpSource.PlayOneShot(PickUpSound);
    }

    public void PlayDeathAudio()
    {
        DeathPSource.PlayOneShot(DeathPSound);
    }

    public void PlayShotgunAudio()
    {
        ShotSource.PlayOneShot(ShotAudio);
    }

    public void PlayDamageAudio()
    {
        DamageSource.PlayOneShot(DamageSound);
    }
#endregion

}
