using System.Collections;
using UnityEngine;


public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerInput PlayerInput;
    public Collider attackCollider;

    private float findTargetRadius = 8f;
    public LayerMask targetLayer;

    [Header("Combo Setting")]
    private float attackComboStep = 0;
    private float comboDelay = 1f;
    private float lastAttackTime;
    private bool attackCombatNext = false;

    [Header("Dash Setting")]
    private bool isDashing;
    private float dashStopDistance = 1.5f;
    private float enemyDistance;
    private float dashSpeed = 10f;


    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        PlayerInput = GetComponent<PlayerInput>();
        attackCollider.enabled = false;
    }
    public void HandleCombatAction()
    {
        if (playerManager != null)
        {            
            FindTargetEnemy();        
            if (playerManager.currentTarget != null && IsEnemyInRanger() )
            {
                if (PlayerInput.SprintInput)
                {
                    StartCoroutine(DashTowardsTarget());
                }
                HandleAttackCombat();
            }
            HandleAttackCombat();
            ResetCombat();

        }
    }
    private void FindTargetEnemy()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, findTargetRadius, targetLayer);
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
            playerManager.currentTarget = nearestTarget;
            playerManager.IsCombatMode = true;
        }
        else
        {
            playerManager.currentTarget = null;
            playerManager.IsCombatMode = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findTargetRadius);
    }
    private void HandleAttackCombat()
    {
        if (Time.time - lastAttackTime > comboDelay)
        {
            attackComboStep = 0;
        }
        if (PlayerInput.CombatInput)
        {
            attackComboStep++;
            if (attackComboStep == 1)
            {
                attackCombatNext = true;
                playerManager.playerAnimatorManager.UpdateAnimatorCombatParameter("Combat1", attackCombatNext);
            }
            else if(attackComboStep == 2)
            {
                attackCombatNext = true;
                playerManager.playerAnimatorManager.UpdateAnimatorCombatParameter("Combat2", attackCombatNext);
            }
            lastAttackTime = Time.time;
        }         
    }
    private void ResetCombat()
    {
        if(attackComboStep == 0)
        {
            attackCombatNext = false;
            playerManager.playerAnimatorManager.UpdateAnimatorCombatParameter("Combat1", attackCombatNext);
            playerManager.playerAnimatorManager.UpdateAnimatorCombatParameter("Combat2", attackCombatNext);
        }       
    }
    public void DeactivateHitbox()
    {
        attackCollider.enabled = false;
    }
    public void ActivateHitbox()
    {
        attackCollider.enabled = true;
    }
    IEnumerator DashTowardsTarget()
    {
        Vector3 startPosition = playerManager.transform.position;
        Vector3 targetPosition = playerManager.currentTarget.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition);

        isDashing = true;

        
        while (Vector3.Distance(playerManager.transform.position, targetPosition) > dashStopDistance && isDashing)
        {        
            Vector3 direction = (targetPosition - playerManager.transform.position).normalized;
            targetRotation = Quaternion.LookRotation(direction);

            playerManager.transform.rotation = Quaternion.Slerp(playerManager.transform.rotation, targetRotation, 300f * Time.deltaTime);
            if (!playerManager.IsPerformAction)
            {
                playerManager.controller.Move(direction * dashSpeed * Time.deltaTime);
            }                                 
            yield return null;
        }
        isDashing = false;
        PlayerInput.SprintInput = false;
    }
    bool IsEnemyInRanger()
    {
        enemyDistance = Vector3.Distance(playerManager.transform.position, playerManager.currentTarget.position);
        if (enemyDistance < findTargetRadius)
        {           
            return true;
        }
        else 
        {          
            return false;
        }       
    }

}
