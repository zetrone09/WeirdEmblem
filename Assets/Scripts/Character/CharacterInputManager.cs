using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    public static CharacterInputManager instance;
    public PlayerManager playerManager;

    CharacterControls characterControls;

    [Header("Movement Input")]
    public Vector2 movementInput;   
    public float horizontalInput;
    public float verticalInput;
    public float moveAmount;
    public bool jumpInput;
    public bool sprintInput;

    [Header("Camera Movement Input")]
    public Vector2 cameraInput;
    public float cameraHorizontalInput;
    public float cameraVerticalInput;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        if (characterControls == null)
        {
            characterControls = new CharacterControls();
            characterControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();           
            characterControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
        }
        characterControls.Enable();
    }
    private void OnDisable()
    {
        characterControls.Disable();
    }
    private void OnApplicationFocus(bool focus)
    {
        if(enabled)
        {
            characterControls.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }
        else
        {
            characterControls.Disable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void Update()
    {
        HandleMovementInput();
        HandleCameraInput();
    }
    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x; 
        verticalInput = movementInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        if (moveAmount > 0.5f && moveAmount <= 1f)
        {           
            if (sprintInput)
            {
                moveAmount = 2;
            }
            else
            {
                moveAmount = 1f;
            }
        }
        playerManager.PlayerAnimatorManager.UpdateAnimationMovementParametor(0, moveAmount);
    }
    private void HandleCameraInput()
    {
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }
}
