using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager characterManager;

    protected virtual void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
    }
    public void UpdateAnimationMovementParametor(float horizontal, float vertical)
    {
       characterManager.Animator.SetFloat("HorizontalMovement", horizontal, 0.1f, Time.deltaTime);
       characterManager.Animator.SetFloat("VerticalMovement", vertical, 0.1f, Time.deltaTime);
    }
}
