using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("InputSystemDeclarations")]
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    [SerializeField] public InputActionReference Move, Aim, Shoot, Interact, Pause, Inventory;
    [Space(5)]

    [Header("GameManagerVariables")]
    public GM_ScriptableObject managerData;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] string cameraObjectName;
    [SerializeField] PlayerSO playerData;
    [Space(5)]

    [Header("PlayerVariables")]
    [SerializeField] float playerSpeed = 3;
    [SerializeField] float playerRotation = 40;
    [SerializeField] float playerAimSpeed = 20;
    [Space(5)]

    [Header("ShootingSettings")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float shootCooldown;
    float shootTimer;
    [Space(5)]

    [Header ("InteractSettings")]
    [SerializeField] float interactRange;
    [SerializeField] LayerMask interactLayer;
    [Space(5)]

    [Header("PlayeStatus")]
    [SerializeField] bool isAim = false; //Checa si el jugador esta apuntando
    [SerializeField] public bool isInteracting = false; //Checa si el jugador esta interactuando
    [SerializeField] bool isTakingDamage = false; //Checa si el jugador esta recibiendo daño
    [Space(5)]

    [Header("AnimationData")]
    Animator pAnimator;
    [SerializeField] List<string> pAnims =  new List<string>(); //En esta lista vamos a meter las animaciones del jugador
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

    private void EnterThirdPerson(InputAction.CallbackContext context)
    {
        if(!isAim && !isInteracting && !isTakingDamage)
        {
            cameraManager.TriggerThirdPerson();
            isAim = true;
        }
        else if(shootTimer == 0 && !isTakingDamage && !isInteracting)
        {
            cameraManager.TriggerThirdPerson();
            isAim = false;
        }
    }

    private void PlayerShoot(InputAction.CallbackContext context)
    {
        if(isAim && shootTimer <= 0 && playerData.playerAmmo > 0 && !isTakingDamage)
        {
            pAnimator.Play(pAnims[5]);
            gunAnimator.Play(gunAnims[2]);
            var bull = Instantiate(bullet, spawnPoint.transform.position, transform.rotation);
            bull.GetComponent<bulletScript>().bulletLife = 5f;
            shootTimer = shootCooldown;
            playerData.playerAmmo--;
        }
        else if (isAim && shootTimer <= 0 && !isTakingDamage)
        {
            pAnimator.Play(pAnims[6]);
            shootTimer = shootCooldown;
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
                    pAnimator.Play(pAnims[7]);
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
            gunAnimator.Play(gunAnims[0]);
            if(playerInput.y == 0 && playerInput.x != 0 && !isInteracting && !isTakingDamage)
            {
                pAnimator.Play(pAnims[3]);
            }



            if(playerInput.y > 0 && !isInteracting && !isTakingDamage)
            {   
                controller.Move(transform.forward * playerInput.y * playerSpeed * Time.deltaTime * managerData.gameTime);
                pAnimator.Play(pAnims[1]);
            }
            else if(playerInput.y < 0 && !isInteracting && !isTakingDamage)
            {
                controller.Move(transform.forward * playerInput.y * (playerSpeed/2) * Time.deltaTime * managerData.gameTime);
                pAnimator.Play(pAnims[2]);
            }


            if(playerInput.y == 0 && playerInput.x == 0 && !isAim && !isInteracting && !isTakingDamage)
            {
                pAnimator.Play(pAnims[0]);
            }
            
            

            transform.Rotate(transform.up, playerRotation * playerInput.x * Time.deltaTime * managerData.gameTime);
        }
        else
        {
            if(isAim && shootTimer == 0 && !isTakingDamage)
            {
                pAnimator.Play(pAnims[4]);
                gunAnimator.Play(gunAnims[1]);
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
            CheckGround();
            PlayerMovement();
        }
    }



    public void TakeDamage()
    {
        isTakingDamage = true;
        playerData.playerHP--;
        if(playerData.playerHP > 0)
        {
            pAnimator.Play(pAnims[8]);
        }
        else
        {
            pAnimator.Play(pAnims[9]);
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

    public void DeathAnimEvent(string DeathScene)
    {
        SceneManager.LoadScene(DeathScene);
    }

#region AnimationEvents
//The following events are used in animation events.
    void Reload()
    {
        shootTimer = 0;
    }

    void StopInteract()
    {
        isInteracting = false;
    }

    void StopDamage()
    {
        isTakingDamage = false;
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
