using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public static CharacterCamera instance;
    public PlayerManager playerManager;

    public Camera cameraObj;
    public Transform cameraPivotTransform;

    [Header("Camera Setting")] 
    private float cameraSmoothSpeed = 1f;
    [SerializeField] float leftAndRightRotationSpeed = 220f;
    [SerializeField] float upAndDownRotationSpeed = 220f;
    [SerializeField] float minimumPivot = -45f;
    [SerializeField] float maximumPivot = 30f;
    [SerializeField] float cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask layerMask;

    [Header("Camera Values")]
    Vector3 cameraVelocity;
    Vector3 cameraObjPosition;
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    private float cameraZPosition;
    private float targetCameraZPosition;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        cameraZPosition = cameraObj.transform.localPosition.z;
    }
    public void HandleAllCameraAction()
    {
        if (playerManager != null)
        {
            HandleFollowTarget();
            HandleRotation();
            HandleCollision();
        }
    }
    private void HandleFollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, playerManager.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetPosition;
    }
    private void HandleRotation()
    {
        leftAndRightLookAngle += (CharacterInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
        upAndDownLookAngle -= (CharacterInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;

        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }
    private void HandleCollision()
    {
        targetCameraZPosition = cameraZPosition;
        RaycastHit hit;
        Vector3 direction = cameraObj.transform.forward - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition),layerMask ))
        { 
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
        }
        if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
        {
            targetCameraZPosition = -cameraCollisionRadius;
        }
        cameraObjPosition.z = Mathf.Lerp(cameraObj.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObj.transform.localPosition = cameraObjPosition;
    }
}
