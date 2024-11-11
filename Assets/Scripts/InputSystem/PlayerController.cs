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


    private void OnEnable()
    {
        Aim.action.performed += EnterThirdPerson;
        Shoot.action.performed += PlayerShoot;
        Interact.action.performed += InteractCall;
        cameraManager = GameObject.Find(cameraObjectName).GetComponent<CameraChanger>();
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
        else
        {
            cameraManager.TriggerThirdPerson();
            isAim = false;
        }
    }

    private void PlayerShoot(InputAction.CallbackContext context)
    {
        if(isAim && shootTimer <= 0)
        {
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
            if(playerInput.y > 0)
            {   
                controller.Move(transform.forward * playerInput.y * playerSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(transform.forward * playerInput.y * (playerSpeed/2) * Time.deltaTime);
            }

            transform.Rotate(transform.up, playerRotation * playerInput.x * Time.deltaTime);
        }
        else
        {
            //transform.Rotate(transform.right, playerAimSpeed * -playerInput.y * Time.deltaTime); //<----Encender a su propio riesgo
            transform.Rotate(transform.up, playerAimSpeed * playerInput.x * Time.deltaTime);
        }
       
        
    }

    
    void Update()
    {
        if(shootTimer > 0)
        {
            shootTimer -= 1*Time.deltaTime;
        }
        playerInput = Move.action.ReadValue<Vector2>();
        PlayerMovement();
    }


    
}
