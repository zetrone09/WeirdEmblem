using UnityEngine;


public class PlayerMotionManager : MonoBehaviour
{
    [Header("Component")]
    PlayerManager playerManager;
    PlayerInput playerInput;
    CameraManager cameraManager;

    private Vector3 moveDirection;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 velocity;
    private float moveSpeed = 7f;


    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerInput = GetComponent<PlayerInput>();
        cameraManager = FindAnyObjectByType<CameraManager>();
    }
    public void HandleAllMovementAction()
    {
        if (playerManager != null && !playerManager.IsPerformAction)
        {
       
            HandleGroundMovement();
            
        }
    }
    private void GetAllMovementValues()
    {
        horizontalMovement = playerInput.horizontalMovementInput;
        verticalMovement = playerInput.verticalMovementInput;
    }
    private void HandleGroundMovement()
    {
        GetAllMovementValues();

        moveDirection = Vector3.zero;
        moveDirection = cameraManager.transform.forward * verticalMovement * Time.deltaTime;
        moveDirection = moveDirection + cameraManager.transform.right * horizontalMovement * Time.deltaTime;
        moveDirection.Normalize();
        moveDirection.y = 0;

        playerManager.controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        playerManager.playerAnimatorManager.UpdateAnimatorGroundMovementParameter(0, playerInput.moveAnimator);

        HandleRotationMovement();
    }
    private void HandleRotationMovement()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
