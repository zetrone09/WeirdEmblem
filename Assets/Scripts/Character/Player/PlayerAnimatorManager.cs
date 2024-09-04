using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    PlayerManager playerManager;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }
    public void UpdateAnimatorGroundMovementParameter(float horizontal,float vertical)
    {
        playerManager.animator.SetFloat("HorizontalMovement",horizontal);
        playerManager.animator.SetFloat("VerticalMovement",vertical);
    }
    public void UpdateAnimatorCombatParameter(string CombatName,bool Combo)
    {
        playerManager.IsPerformAction = Combo;
        playerManager.animator.SetBool(CombatName, Combo);
    }
}
