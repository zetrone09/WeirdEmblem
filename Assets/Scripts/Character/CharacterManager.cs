using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : MonoBehaviour
{
    public CharacterController Controller;
    public Animator Animator;
    protected virtual void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        
    }
}
