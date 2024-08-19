using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class StateMachineManger : MonoBehaviour
{
    public IState currentState;

    [Header("State")]
    public IdleState idleState = new IdleState();
    public RunState runState = new RunState();
    public CombatState combatState = new CombatState();

    [Header("Component")]
    private CharacterController characterController;
    private CharacterInput playerInput;
    private Animator playerAnimator;

    private void Start()
    {
        playerInput = GetComponent<CharacterInput>();
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentState = idleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this, playerInput); 
        currentState.UpdateAnimatorParameterState(this,playerInput);
    }

    public void SwitchState(IState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
    public void SetMovementAnimatorParameter(string parameterName, float value)
    {
        playerAnimator.SetFloat(parameterName, value);
    }
    public void SetCombatAnimatorParameter(string parameterName, bool value)
    {
        playerAnimator.SetBool(parameterName, value);
    }
    public void HandleGroundMovement(Vector3 MovementDirection)
    {
        if (characterController.isGrounded)
        {
            characterController.Move(MovementDirection * Time.deltaTime);
        }
        else 
        {
            MovementDirection = Vector3.zero;
            MovementDirection.y -= 9.8f * Time.deltaTime;
            characterController.Move(MovementDirection);
        }
    }
}
