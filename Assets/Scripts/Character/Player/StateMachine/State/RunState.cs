using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private float moveSpeed = 5f;
    public void EnterState(StateMachineManger stateMachine)
    {
        Debug.Log("Enter Run State");
    }

    public void ExitState(StateMachineManger stateMachine)
    {
        Debug.Log("Exit Run State");
    }

    public void UpdateAnimatorParameterState(StateMachineManger stateMachine, CharacterInput input)
    {
        stateMachine.SetMovementAnimatorParameter("HorizontalMovement", input.MovementInput.x);
        stateMachine.SetMovementAnimatorParameter("VerticalMovement", input.MovementInput.y);
    }

    public void UpdateState(StateMachineManger stateMachine, CharacterInput input)
    {
        if (input.MovementInput == Vector2.zero)
        {
            stateMachine.SwitchState(stateMachine.idleState);
        }
        else if (input.AttackInput)
        {
            stateMachine.SwitchState(stateMachine.combatState);
        }
        else
        {
            Vector3 moveDirection = new Vector3(input.MovementInput.x, 0, input.MovementInput.y);
            stateMachine.HandleGroundMovement(moveDirection * moveSpeed);
        }
    }
    
}
