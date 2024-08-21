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

    public void UpdateAnimatorMovementParameter(float horizontal,float vertical)
    {
        playerManager.animator.SetFloat("HorizontalMovement", horizontal);
        playerManager.animator.SetFloat("VerticalMovement", vertical);
    }
    public void UpdateAnimatorCombatParameter(bool attack)
    {
        playerManager.animator.SetBool("Combat1", attack);
    }
}
