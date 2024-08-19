using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : IState
{
    private float moveSpeed = 5f;
    public void EnterState(StateMachineManger stateMachine)
    {
        Debug.Log("Enter Combat State");
    }

    public void ExitState(StateMachineManger stateMachine)
    {
        Debug.Log("Exit Combat State");
    }

    public void UpdateAnimatorParameterState(StateMachineManger stateMachine, CharacterInput input)
    {
        stateMachine.SetMovementAnimatorParameter("HorizontalMovement", input.MovementInput.x);
        stateMachine.SetMovementAnimatorParameter("VerticalMovement", input.MovementInput.y);
        stateMachine.SetCombatAnimatorParameter("Attack1", input.AttackInput);
    }

    public void UpdateState(StateMachineManger stateMachine, CharacterInput input)
    {

          
    }
}
