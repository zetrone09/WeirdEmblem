using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerManager playerManager;
    public Camera cameraObject;
    public Transform cameraPivotTransform;

    [Header("Camera Follow Setting")]
    private Vector3 cameraVelocity;
    [SerializeField]private float cameraSmoothSpeed = 1f;


    [Header("Camera Rotation Setting")]
    public float horizontalLook;
    public float verticalLook;
    private float rotationLookSpeed = 220f;
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
        playerInput = FindAnyObjectByType<PlayerInput>();
        playerManager = FindAnyObjectByType<PlayerManager>();
        cameraObject = Camera.main;
        defaultCameraZPosition = cameraObject.transform.localPosition.z;
    }
    private void Update()
    {
        HandleAllCameraAction();
    }
    void HandleAllCameraAction()
    {     
        FollowCameraAction();
        if (playerManager.IsCombatMode) 
        {
            CameraLockTargetAction();    
        }
        else
        {
            RotationCameraAction();
        }                                  
        CameraCollisitionAction();             
    }
    private void FollowCameraAction()
    {
        Vector3 smoothDirection = Vector3.SmoothDamp(transform.transform.position,playerManager.transform.position,ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = smoothDirection;
    }
    private void RotationCameraAction()
    {
        horizontalLook += playerInput.CameraInput.x * rotationLookSpeed * Time.deltaTime;
        verticalLook -= playerInput.CameraInput.y * rotationLookSpeed * Time.deltaTime;
        verticalLook = Mathf.Clamp(verticalLook, minimumAngle, maximunAngle);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion rotationCamera;

        cameraRotation.y = horizontalLook ;
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
    private void CameraLockTargetAction()
    {
        if (playerManager.currentTarget != null)
        {
            Vector3 direction = playerManager.currentTarget.position - transform.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationLookSpeed * Time.deltaTime);
            horizontalLook += transform.rotation.y;
            verticalLook += transform.rotation.x;
        }       
    }
    


}
