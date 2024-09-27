using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("InputSystemDeclarations")]
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    [SerializeField] InputActionReference Move, Aim, Shoot;

    [Header("GameManagerVariables")]
    [SerializeField] CameraChanger cameraManager;
    [SerializeField] string cameraObjectName;

    [Header("PlayerVariables")]
    [SerializeField] float playerSpeed = 3;
    [SerializeField] float playerRotation = 40;
    [SerializeField] float playerAimSpeed = 20;
    

    [Header("PlayeStatus")]
    [SerializeField] bool isAim = false; //Checa si el jugador esta apuntando


    private void OnEnable()
    {
        Aim.action.performed += EnterThirdPerson;
        Shoot.action.performed += PlayerShoot;
        cameraManager = GameObject.Find(cameraObjectName).GetComponent<CameraChanger>();
    }

    private void OnDisable()
    {
        Aim.action.performed -= EnterThirdPerson;
        Shoot.action.performed -= PlayerShoot;

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
        if(isAim)
        {
            Debug.Log("PlayerShot");
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
        playerInput = Move.action.ReadValue<Vector2>();
        PlayerMovement();
    }
}
