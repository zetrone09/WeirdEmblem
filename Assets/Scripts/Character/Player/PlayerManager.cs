using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public PlayerMotionManager playerMotionManager;
    public PlayerAnimatorManager playerAnimatorManager;
    public PlayerCombatManager playerCombatManager;
    protected override void Awake()
    {
        base.Awake();
        playerMotionManager = GetComponent<PlayerMotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
    }
    private void Start()
    {
        
    }
    protected override void Update()
    {
        base.Update();
        playerMotionManager.HandleAllMovementAction();
    }
}
