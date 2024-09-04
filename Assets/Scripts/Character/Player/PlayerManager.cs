using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMotionManager playerMotionManager { get; private set; }
    public PlayerAnimatorManager playerAnimatorManager { get; private set; }
    public PlayerCombatManager playerCombatManager { get; private set; }
    public bool IsCombatMode { get; set; } = false;

    public bool IsPerformAction = false;

    public Transform currentTarget;
    public CharacterController controller { get; private set; }
    public Animator animator { get; private set; }
    protected void Awake()
    {
        playerMotionManager = GetComponent<PlayerMotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    protected void Update()
    {
        playerMotionManager.HandleAllMovementAction();
        playerCombatManager.HandleCombatAction();
    }
}
