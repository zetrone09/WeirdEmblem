using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;

    public PlayerManager player;
    public Transform cameraPivotTransform;
    public Camera CameraObj;
    public float horizontalLook;
    public float verticalLook;
    public float horizontalLookSpeed;
    public float verticalLookSpeed;
    public float maximumAngle;
    public float minimumAngle;
    public float cameraSmoothSpeed;
    Vector3 cameraVelocity;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }
    private void Update()
    {
        CameraFollow();
        CameraRotation();
        CameraCollsion();
    }
    void CameraFollow()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }
    void CameraRotation()
    {
        horizontalLook += (CharacterInputManager.instance.cameraHorizontalInput * horizontalLookSpeed) * Time.deltaTime;
        verticalLook -= (CharacterInputManager.instance.cameraVerticalInput * verticalLookSpeed) * Time.deltaTime;
        verticalLook = Mathf.Clamp(verticalLook, minimumAngle, maximumAngle);

        Vector3 cameraRotate = Vector3.zero;
        Quaternion targetRotate;

        cameraRotate.y = horizontalLook;
        targetRotate = Quaternion.Euler(cameraRotate);
        transform.rotation = targetRotate;

        cameraRotate = Vector3.zero;
        cameraRotate.x = verticalLook;
        targetRotate = Quaternion.Euler(cameraRotate);
        cameraPivotTransform.localRotation = targetRotate;
    }
    void CameraCollsion()
    {

    }
}
