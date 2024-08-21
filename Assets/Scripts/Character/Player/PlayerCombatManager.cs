using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerInput playerInput;
    CameraManager cameraManager;

    [Header("Target Lock")]
    private bool isLock = false;
    public Transform currentTarget;
    private float lockOnRadius = 10f;
    public LayerMask TargetLayerMask;

    [Header("Dash Setting")]
    public float dashSpeed = 5f;
    public float dashDistance = 5f;
    public float dashCooldown = 1f;
    public float dashStopDistance = 1f;
    public bool isDashing = false;
    public bool canDash = true;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerInput = GetComponent<PlayerInput>();
        cameraManager = FindAnyObjectByType<CameraManager>();
    }
    private void Update()
    {
        if (playerInput != null)
        {
            TargetLock();
            if (currentTarget != null)
            {
                if (playerInput.CombatInput && !isDashing)
                {
                    StartCoroutine(DashTowardsTarget());

                }
                else if (Vector3.Distance(transform.position, currentTarget.position) <= dashStopDistance)
                {
                    playerManager.playerAnimatorManager.UpdateAnimatorCombatParameter(playerInput.CombatInput);
                }
            }          
            if (playerInput.SprintInput && canDash && !playerInput.CombatInput)
            {
                Dash();
            }
        }
    }
    void TargetLock()
    {
        isLock = playerInput.isLock;
        if (isLock)
        {
            if (currentTarget == null)
            {
                FindTarget();
            }
            else
            {
                UnlockTarget();
            }
        }

        if (currentTarget != null)
        {
            LockOnTarget();
        }
    }
    void FindTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, lockOnRadius, TargetLayerMask);

        float closestDistance = Mathf.Infinity;
        Transform nearestTarget = null;

        foreach (Collider target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                nearestTarget = target.transform;
            }
        }

        if (nearestTarget != null)
        {
            currentTarget = nearestTarget;
            isLock = false;
        }
    }
    void LockOnTarget()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.position - cameraManager.transform.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            cameraManager.transform.rotation = Quaternion.Slerp(cameraManager.transform.rotation, targetRotation, Time.deltaTime);
        }
    }
    void UnlockTarget()
    {
        currentTarget = null;
        isLock = false;
    }
    IEnumerator DashTowardsTarget()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = currentTarget.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition);

        isDashing = true;
        if (Vector3.Distance(startPosition, targetPosition) > dashStopDistance)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            playerManager.transform.rotation = Quaternion.Slerp(cameraManager.transform.rotation, targetRotation, Time.deltaTime);
            playerManager.characterController.Move(direction * dashSpeed * Time.deltaTime);
            yield return null;
        }
        
        isDashing = false;
    }
    void Dash()
    {
        Vector3 dashPosition = transform.position + transform.forward * dashDistance;

        if (!Physics.Raycast(transform.position, transform.forward, dashDistance))
        {
            transform.position = dashPosition;
        }
        StartCoroutine(DashCooldown());
    }
    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
