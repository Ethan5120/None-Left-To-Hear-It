using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
}

public class PlayerController : MonoBehaviour
{
    [Header("InputSystemDeclarations")]
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    [SerializeField] InputActionReference Move, Aim, Shoot, Interact;

    [Header("GameManagerVariables")]
    [SerializeField] CameraChanger cameraManager;
    [SerializeField] string cameraObjectName;

    [Header("PlayerVariables")]
    [SerializeField] float playerSpeed = 3;
    [SerializeField] float playerRotation = 40;
    [SerializeField] float playerAimSpeed = 20;

    [Header("ShootingSettings")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float shootCooldown;
    float shootTimer;

    [Header ("InteractSettings")]
    [SerializeField] float interactRange;
    [SerializeField] LayerMask interactLayer;
    

    [Header("PlayeStatus")]
    [SerializeField] bool isAim = false; //Checa si el jugador esta apuntando

    [Header("AnimationData")]
    Animator pAnimator;
    [SerializeField] List<string> pAnims =  new List<string>(); //En esta lista vamos a meter las animaciones del jugador


    private void OnEnable()
    {
        Aim.action.performed += EnterThirdPerson;
        Shoot.action.performed += PlayerShoot;
        Interact.action.performed += InteractCall;
        cameraManager = GameObject.Find(cameraObjectName).GetComponent<CameraChanger>();
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
        if(!isAim)
        {
            cameraManager.TriggerThirdPerson();
            isAim = true;
        }
        else if(shootTimer == 0)
        {
            cameraManager.TriggerThirdPerson();
            isAim = false;
        }
    }

    private void PlayerShoot(InputAction.CallbackContext context)
    {
        if(isAim && shootTimer <= 0)
        {
            pAnimator.Play(pAnims[5]);
            var bull = Instantiate(bullet, spawnPoint.transform.position, transform.rotation);
            bull.GetComponent<bulletScript>().bulletLife = 5f;
            shootTimer = shootCooldown;
        }
    }

    private void InteractCall(InputAction.CallbackContext context)
    {
        Ray r = new Ray (transform.position, transform.forward);
        if(Physics.Raycast(r, out RaycastHit hitInfo, interactRange, interactLayer))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObject))
            {
                interactObject.Interact();
            }
        }
    }

    private void PlayerMovement()
    {
        //Use the move method to move the player to the front and back
        if(!isAim)
        {
            if(playerInput.y == 0 && playerInput.x != 0)
            {
                pAnimator.Play(pAnims[3]);
            }



            if(playerInput.y > 0)
            {   
                controller.Move(transform.forward * playerInput.y * playerSpeed * Time.deltaTime);
                pAnimator.Play(pAnims[1]);
            }
            else if(playerInput.y < 0)
            {
                controller.Move(transform.forward * playerInput.y * (playerSpeed/2) * Time.deltaTime);
                pAnimator.Play(pAnims[2]);
            }


            if(playerInput.y == 0 && playerInput.x == 0 && !isAim)
            {
                pAnimator.Play(pAnims[0]);
            }
            
            

            transform.Rotate(transform.up, playerRotation * playerInput.x * Time.deltaTime);
        }
        else
        {
            if(isAim && shootTimer == 0)
            {
                pAnimator.Play(pAnims[4]);
            }
            //transform.Rotate(transform.right, playerAimSpeed * -playerInput.y * Time.deltaTime); //<----Encender a su propio riesgo
            transform.Rotate(transform.up, playerAimSpeed * playerInput.x * Time.deltaTime);
        }
       
        
    }

    
    void Update()
    {
        playerInput = Move.action.ReadValue<Vector2>();
        PlayerMovement();
    }


    void Reload()
    {
        shootTimer = 0;
    }
}
