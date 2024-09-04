using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : MonoBehaviour
{
    public CharacterController controller { get; private set; }
    public Animator animator { get; private set; }
    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        
    }
}
