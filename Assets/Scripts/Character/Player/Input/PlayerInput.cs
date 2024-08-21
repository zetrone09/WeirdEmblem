using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    CharacterControls playerInput;

    [Header("Player Input")]
    public Vector2 MovementInput;
    public Vector2 CameraInput;
    public bool CombatInput;
    public bool JumpInput = false;
    public bool SprintInput = false;
    public bool isLock = false;

    public float moveAnimator;
    private void Awake()
    {
        if (playerInput == null) 
        { 
            playerInput = new CharacterControls();
            playerInput.PlayerMovement.Movement.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
            playerInput.PlayerMovement.Movement.canceled += ctx => MovementInput = Vector2.zero;

            playerInput.PlayerCamera.Movement.performed += ctx => CameraInput = ctx.ReadValue<Vector2>();
            playerInput.PlayerCamera.Movement.canceled += ctx => CameraInput = Vector2.zero;

            playerInput.PlayerMovement.Attack.performed += ctx => CombatInput = true;
            playerInput.PlayerMovement.Attack.canceled += ctx => CombatInput = false;

            playerInput.PlayerMovement.Jump.performed += ctx => JumpInput = true;
            playerInput.PlayerMovement.Jump.canceled += ctx => JumpInput = false;

            playerInput.PlayerMovement.Sprint.started += ctx => SprintInput = true;
            playerInput.PlayerMovement.Sprint.canceled += ctx => SprintInput = false;

            playerInput.PlayerCamera.LockTarget.started += ctx => isLock = true;
            playerInput.PlayerCamera.LockTarget.canceled += ctx => isLock = false;
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            playerInput.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerInput.Disable();
        }
    }
    private void Update()
    {
        UpdateMoveAnimator();
    }
    public void UpdateMoveAnimator()
    {
        if (SprintInput && MovementInput.sqrMagnitude > 0)
        {
            moveAnimator = 1f;
        }
        else if (MovementInput.sqrMagnitude > 0)
        {
            moveAnimator = 0.5f;
        }
        else 
        { 
            moveAnimator = 0f;
        }

    }


}
