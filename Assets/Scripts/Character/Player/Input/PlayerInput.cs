using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerManager playerManager;
    CharacterControls playerInput;
    public Vector2 MovementInput { get; private set; }
    public float horizontalMovementInput { get; private set; }
    public float verticalMovementInput { get; private set; }
    public float moveAnimator { get; private set; }
    public Vector2 CameraInput { get; private set; }
    public bool CombatInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool SprintInput { get; set; }
    public bool isLock { get; private set; } = false;
    
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        if (playerInput == null) 
        { 
            playerInput = new CharacterControls();
            playerInput.PlayerMovement.Movement.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
            playerInput.PlayerMovement.Movement.canceled += ctx => MovementInput = Vector2.zero;

            playerInput.PlayerCamera.Movement.performed += ctx => CameraInput = ctx.ReadValue<Vector2>();
            playerInput.PlayerCamera.Movement.canceled += ctx => CameraInput = Vector2.zero;

            playerInput.PlayerMovement.Attack.performed += ctx => CombatInput = true;
            playerInput.PlayerMovement.Attack.canceled += ctx => CombatInput = false;

            playerInput.PlayerMovement.Jump.performed += ctx => JumpInput = true;

            playerInput.PlayerMovement.Sprint.started += ctx => SprintInput = true;

            playerInput.PlayerCamera.LockTarget.started += ctx => isLock = true;
        }
    }
    private void OnEnable()
    {
        playerInput.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerInput.Disable();
    }
    private void Update()
    {
        HandleGroundMovementInput();
    }
    private void HandleGroundMovementInput()
    {
        horizontalMovementInput = MovementInput.x;
        verticalMovementInput = MovementInput.y;
        moveAnimator = Mathf.Abs(Mathf.Abs(horizontalMovementInput) + Mathf.Abs(verticalMovementInput));

        if (moveAnimator > 0 && moveAnimator <= 0.5f) 
        {
            moveAnimator = 0.5f;
        }
        else if(moveAnimator > 0.5f && moveAnimator <= 1f)
        {
            moveAnimator = 1f;
        }
    }

}
