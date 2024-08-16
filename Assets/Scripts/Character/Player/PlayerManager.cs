using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    CharacterControls characterControls;
    CharacterController characterController;
    Animator animator;

    private Vector3 moveDirection;
    private float moveSpeed = 5f;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 rotationDirection;   
    private float rotationSpeed = 5f;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
    }
    
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }
    void GetMovementInputValue()
    {
        horizontalInput = CharacterInputManager.instance.horizontalInput;
        verticalInput = CharacterInputManager.instance.verticalInput;
    }
    void HandleMovement()
    {
        GetMovementInputValue();
        if (characterController.isGrounded)
        {
            moveDirection = Vector3.zero;
            moveDirection = PlayerCamera.instance.CameraObj.transform.forward * verticalInput;
            moveDirection = moveDirection + PlayerCamera.instance.CameraObj.transform.right * horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;
        }
       
        moveDirection.y -= Time.deltaTime;

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);  
            
        animator.SetFloat("VerticalMovement", verticalInput);
    }
    void HandleRotation()
    {
        rotationDirection = Vector3.zero;
        rotationDirection = PlayerCamera.instance.CameraObj.transform.forward * verticalInput;
        rotationDirection = rotationDirection + PlayerCamera.instance.CameraObj.transform.right * horizontalInput;
        rotationDirection.Normalize();
        rotationDirection.y = 0;

        if (rotationDirection == Vector3.zero) 
        { 
            rotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(rotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation,rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
}
