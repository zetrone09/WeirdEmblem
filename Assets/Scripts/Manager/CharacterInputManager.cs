using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    public static CharacterInputManager instance;

    CharacterControls characterInput;

    public Vector2 movementInput;
    public float horizontalInput;
    public float verticalInput;

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
    private void OnEnable()
    {
        if (characterInput == null)
        {
            characterInput = new CharacterControls();
            characterInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            characterInput.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
        }
        characterInput.Enable();
    }
    private void OnDisable()
    {
        characterInput.PlayerMovement.Movement.canceled -= i => movementInput = i.ReadValue<Vector2>();
        characterInput.Disable();
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        MovementInput();
        CameraInput();
    }
    void MovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
    }
    void CameraInput()
    {
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }
}
