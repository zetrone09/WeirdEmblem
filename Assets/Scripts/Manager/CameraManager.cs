using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerManager PlayerManager;
    public PlayerCombatManager playerCombatManager;
    public Camera cameraObject;
    public Transform cameraPivotTransform;

    [Header("Camera Follow Setting")]
    private Vector3 cameraVelocity;
    [SerializeField]private float cameraSmoothSpeed = 1f;

    [Header("Camera Rotation Setting")]
    private float horizontalLook;
    private float verticalLook;
    private float horizontalLookSpeed = 220f;
    private float verticalLookSpeed = 220f;
    private float maximunAngle = 30f;
    private float minimumAngle = -30f;

    [Header("Camera Collisiion Setting")]
    private Vector3 cameraObjectPosition;
    private float defaultCameraZPosition;
    private float targetCameraZPosition;
    private float cameraCollisionRadius = 0.2f;
    public LayerMask CollisionlayerMask;


    private void Awake()
    {
        PlayerManager = FindAnyObjectByType<PlayerManager>();
        playerInput = FindAnyObjectByType<PlayerInput>();
        playerCombatManager = FindAnyObjectByType<PlayerCombatManager>();
    }
    private void Start()
    {
        defaultCameraZPosition = cameraObject.transform.localPosition.z;
    }
    private void Update()
    {
        HandleCameraAction();
    }
    void HandleCameraAction()
    {
        if (PlayerManager != null) 
        {
            FollowCameraAction();
            if (playerCombatManager.currentTarget == null)
            {
                RotationCameraAction();
            }
            CameraCollisitionAction();
        }       
    }
    private void FollowCameraAction()
    {
        Vector3 smoothDirection = Vector3.SmoothDamp(transform.transform.position,PlayerManager.transform.position,ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = smoothDirection;
    }
    private void RotationCameraAction()
    {
        horizontalLook += playerInput.CameraInput.x * horizontalLookSpeed * Time.deltaTime;
        verticalLook -= playerInput.CameraInput.y * verticalLookSpeed * Time.deltaTime;
        verticalLook = Mathf.Clamp(verticalLook, minimumAngle, maximunAngle);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion rotationCamera;

        cameraRotation.y = horizontalLook;
        rotationCamera = Quaternion.Euler(cameraRotation);
        transform.rotation = rotationCamera;

        cameraRotation = Vector3.zero;
        cameraRotation.x = verticalLook;
        rotationCamera = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.transform.localRotation = rotationCamera;
    }
    private void CameraCollisitionAction()
    {
        targetCameraZPosition = defaultCameraZPosition;

        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius,direction,out hit,Mathf.Abs(targetCameraZPosition), CollisionlayerMask))
        {
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position,hit.point);
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
        }
        if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
        {
            targetCameraZPosition = -cameraCollisionRadius;
        }
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
    
}
