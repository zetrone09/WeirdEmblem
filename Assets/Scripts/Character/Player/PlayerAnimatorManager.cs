using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public PlayerManager player;
    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    public void UpdateAnimationMovementParameter(float horizontal,float vertical)
    {
        player.Animator.SetFloat("VerticalMovement", horizontal);
        player.Animator.SetFloat("VerticalMovement", vertical);
    }
}
