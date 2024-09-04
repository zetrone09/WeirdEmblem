using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHitBox : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;
    public GameObject hitVFX;
    public GameObject wallHitVFX;
    public LayerMask wallLayerMask;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CharacterController monsterController = other.GetComponent<CharacterController>();

            if (monsterController != null)
            {
                Vector3 knockbackDirection = other.transform.position - transform.position;
                knockbackDirection.y = 0;
                knockbackDirection.Normalize();

                Instantiate(hitVFX, other.ClosestPoint(transform.position), Quaternion.identity);

                Vector3 knockbackPosition = other.transform.position + knockbackDirection * knockbackForce;
                StartCoroutine(Knockback(monsterController, knockbackPosition, knockbackDirection));
            }
        }
    }
    private IEnumerator Knockback(CharacterController monsterController, Vector3 knockbackPosition, Vector3 knockbackDirection)
    {
        float elapsedTime = 0f;

        while (elapsedTime < knockbackDuration)
        {
            monsterController.Move((knockbackPosition - monsterController.transform.position) * Time.deltaTime / knockbackDuration);
            elapsedTime += Time.deltaTime;

            RaycastHit hit;
            if (Physics.Raycast(monsterController.transform.position, knockbackDirection, out hit, 1f, wallLayerMask))
            {
                Instantiate(wallHitVFX, hit.point, Quaternion.identity);
                break; 
            }

            yield return null;
        }
    }

}
