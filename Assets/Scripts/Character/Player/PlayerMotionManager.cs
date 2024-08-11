using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionManager : MonoBehaviour
{
    public CharacterControls characterControls;
    public PlayerManager player;
    public PlayerAnimatorManager playerAnimatorManager;

    [Header("Movement Setting")]
    private Vector3 movementDirection;
    [SerializeField] float moveSpeed = 2f;

    [Header("Input Setting")]
    Vector2 movementInput;
    private void OnEnable()
    {
        if (characterControls == null)
        {
            characterControls = new CharacterControls();
            characterControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        characterControls.Enable();
    }
    private void OnDisable()
    {
        characterControls.Disable();
    }
    public void HandleAllMovement()
    {
        movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
        movementDirection.Normalize();
        
        player.Controller.Move(movementDirection * moveSpeed * Time.deltaTime);

        playerAnimatorManager.UpdateAnimationMovementParameter(movementInput.x,movementInput.y);

    }
}
