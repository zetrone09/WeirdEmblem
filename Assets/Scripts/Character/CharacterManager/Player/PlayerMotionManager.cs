using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionManager : CharacterMotionManager
{
    PlayerManager playerManager;
    PlayerAnimatorManager playerAnimatorManager;

    [Header("Movement")]
    private Vector2 _movement;
    private float _horizontalMovement;
    private float _verticalMovement;
    private float _moveAmount;

    [Header("Movement Ground Setting")]
    public Vector3 MovementDirection;
    public float MoveSpeed = 2f;
    public float RunSpeed = 5f;

    [Header("Movement Air Setting")]
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumping;

    [Header("Rotation Setting")]
    public Vector3 MovementRotation;
    public float RotationSpeed = 15f;
    protected override void Awake()
    {
        base.Awake();

        playerManager = GetComponent<PlayerManager>();
        playerAnimatorManager = GetComponent <PlayerAnimatorManager>();
    }
    public void HandleAllMovement() 
    {
        GroundCheack();
        if (playerManager.characterController.isGrounded)
        {
            HandleGroundMovement();
        }
        
        HandleRotation();
    }
    public void GetAllInputValues()
    {
        _horizontalMovement = CharacterInputManager.instance.horizontalInput;
        _verticalMovement = CharacterInputManager.instance.verticalInput;
        isJumping = CharacterInputManager.instance.jumpInput;
    }
    public void HandleGroundMovement()
    {
        GetAllInputValues();
        MovementDirection = CharacterCamera.instance.transform.forward * _verticalMovement;
        MovementDirection = MovementDirection + CharacterCamera.instance.transform.right * _horizontalMovement;
        MovementDirection.Normalize();
        MovementDirection.y = 0;

        if (CharacterInputManager.instance.moveAmount <= 1f)
        {
            playerManager.characterController.Move(MovementDirection * MoveSpeed * Time.deltaTime);
        }
        else if (CharacterInputManager.instance.moveAmount > 1f)
        {
            playerManager.characterController.Move(MovementDirection * RunSpeed * Time.deltaTime);
        }        
    }
    public void GroundCheack()
    {
        isGrounded = playerManager.characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isJumping && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        playerManager.characterController.Move(velocity * Time.deltaTime);
    }
    public void HandleRotation() 
    {
        MovementRotation = Vector3.zero;
        MovementRotation = CharacterCamera.instance.transform.forward * _verticalMovement;
        MovementRotation = MovementRotation + CharacterCamera.instance.transform.right * _horizontalMovement;
        MovementDirection.Normalize();
        MovementDirection.y = 0;

        if (MovementRotation == Vector3.zero)
        {
            MovementRotation = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(MovementRotation);
        Quaternion movementRotation = Quaternion.Slerp(transform.rotation, newRotation, RotationSpeed * Time.deltaTime);
        transform.rotation = movementRotation;
    }
}
