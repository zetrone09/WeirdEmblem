using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GridBrushBase;

public class PlayerMotionManager : MonoBehaviour
{
    PlayerInput PlayerInput;
    PlayerManager PlayerManager;
    PlayerAnimatorManager PlayerAnimatorManager;
    CameraManager CameraManager;

    private Vector2 movementInput;
    private bool jumpInput = false;

    public bool isGrounded;
    private Vector3 moveDirection;
    private Vector3 velocity;
    public Vector3 rotationDirection;
    private float gravity = -1f;
    private float jumpForce = 0.2f;
    private float moveSpeed = 7f;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        PlayerManager = GetComponent<PlayerManager>();
        PlayerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        CameraManager = FindAnyObjectByType<CameraManager>();
    }
    public void HandleAllMovementAction()
    {
        GetAllInputAction();
        if (PlayerManager.playerCombatManager.canDash && !PlayerInput.CombatInput)
        {
            HandleGroundMovementAction();
            HandleRotationAction();
        }            
    }
    private void GetAllInputAction()
    {
        movementInput = PlayerInput.MovementInput;
        jumpInput = PlayerInput.JumpInput;
    }
    private void HandleGroundMovementAction()
    {
        isGrounded = PlayerManager.characterController.isGrounded;
        if (isGrounded && velocity.y < 0f) 
        {
            velocity.y = -2f;
        }
        if (velocity.z > 0f)
        {
            velocity.z = 0f;
        }

        moveDirection = Vector3.zero;
        moveDirection = CameraManager.cameraObject.transform.forward * movementInput.y;
        moveDirection = moveDirection + CameraManager.cameraObject.transform.right * movementInput.x;
        moveDirection.Normalize();
        moveDirection.y = 0f;


        PlayerManager.characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        PlayerAnimatorManager.UpdateAnimatorMovementParameter(0, PlayerInput.moveAnimator);
        

        if (isGrounded && jumpInput) 
        { 
            velocity.y = Mathf.Sqrt(jumpForce * gravity * -0.5f);
        }

        velocity.y +=  gravity/4 * Time.deltaTime;
        PlayerManager.characterController.Move(velocity);
    }
    private void HandleRotationAction()
    {
        rotationDirection = Vector3.zero;
        rotationDirection = CameraManager.cameraObject.transform.forward * movementInput.y;
        rotationDirection = moveDirection + CameraManager.cameraObject.transform.right * movementInput.x;

        if (rotationDirection == Vector3.zero)
        {
            rotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(rotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime);
        transform.rotation = newRotation;

    }
}
