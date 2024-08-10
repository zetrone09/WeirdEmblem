using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public PlayerAnimatorManager PlayerAnimatorManager;
    public PlayerMotionManager playerMotionManager;
    
    protected override void Awake()
    {
        base.Awake();
        playerMotionManager = GetComponent<PlayerMotionManager>();
        PlayerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        CharacterCamera.instance.playerManager = this;
        CharacterInputManager.instance.playerManager = this;
    }
    protected override void Update()
    {
        base.Update();
        playerMotionManager.HandleAllMovement();
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        CharacterCamera.instance.HandleAllCameraAction();
        
    }
}
