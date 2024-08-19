using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
     void EnterState(StateMachineManger stateMachine);
     void UpdateState(StateMachineManger stateMachine,CharacterInput input);
     void ExitState(StateMachineManger stateMachine);
     void UpdateAnimatorParameterState(StateMachineManger stateMachine,CharacterInput input);

}
