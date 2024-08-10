using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterController characterController;
    public Animator Animator;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        characterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
    }
    protected virtual void Update() 
    { 

    }
    protected virtual void LateUpdate()
    {

    }

}
