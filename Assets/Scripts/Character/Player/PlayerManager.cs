using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    PlayerMotionManager playerMotionManager;
    PlayerAnimatorManager playerAnimatorManager;
    protected override void Awake()
    {
        base.Awake();
        playerMotionManager = GetComponent<PlayerMotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();

        playerMotionManager.player = this;
        playerAnimatorManager.player = this;
        playerMotionManager.playerAnimatorManager = playerAnimatorManager;
    }
    protected override void Update()
    {
        base.Update();
        playerMotionManager.HandleAllMovement();
    }
}
