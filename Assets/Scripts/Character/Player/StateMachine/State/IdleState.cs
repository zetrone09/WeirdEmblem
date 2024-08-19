
using UnityEngine;
using UnityEngine.TextCore.Text;

public class IdleState : IState
{
    public void EnterState(StateMachineManger stateMachine)
    {
        Debug.Log("Enter Idle State");
    }

    public void ExitState(StateMachineManger stateMachine)
    {
        Debug.Log("Exit Idle State");
    }

    public void UpdateAnimatorParameterState(StateMachineManger stateMachine, CharacterInput input)
    {
        stateMachine.SetMovementAnimatorParameter("HorizontalMovement", input.MovementInput.x);
        stateMachine.SetMovementAnimatorParameter("VerticalMovement", input.MovementInput.y);
    }

    public void UpdateState(StateMachineManger stateMachine, CharacterInput input)
    {
        if (input.MovementInput != Vector2.zero)
        {
            stateMachine.SwitchState(stateMachine.runState);
            
        }
        else if (input.AttackInput)
        {
            stateMachine.SwitchState(stateMachine.combatState);
        }
        else 
        {
            stateMachine.HandleGroundMovement(input.MovementInput);
        }
    }


}
