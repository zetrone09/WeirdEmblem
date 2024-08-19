using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    CharacterControls playerControls;

    public Vector2 MovementInput;
    public Vector2 CameraInput;

    public bool JumpInput;
    public bool AttackInput;

    private void Awake()
    {
        playerControls = new CharacterControls();

        playerControls.PlayerMovement.Movement.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
        playerControls.PlayerMovement.Movement.canceled += ctx => MovementInput = Vector2.zero;

        playerControls.PlayerMovement.Jump.performed += ctx => JumpInput = true;
        playerControls.PlayerMovement.Jump.canceled += ctx => JumpInput = false;

        playerControls.PlayerMovement.Attack.performed += ctx => AttackInput = true;
        playerControls.PlayerMovement.Attack.canceled += ctx => AttackInput = false;
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
}
