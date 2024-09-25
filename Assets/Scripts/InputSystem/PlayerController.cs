using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    [SerializeField] float playerSpeed = 3;
    [SerializeField] float playerRotation = 40;
    private void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    private void PlayerMovement()
    {
        //Use the move method to move the player to the front and back
        if(playerInput.y > 0)
        {   
            controller.Move(transform.forward * playerInput.y * playerSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(transform.forward * playerInput.y * (playerSpeed/2) * Time.deltaTime);
        }
       
        
        //Rotate the player
        transform.Rotate(transform.up, playerRotation * playerInput.x * Time.deltaTime);
    }

    
    void Update()
    {
        PlayerMovement();
    }
}
