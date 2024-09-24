using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    private void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
