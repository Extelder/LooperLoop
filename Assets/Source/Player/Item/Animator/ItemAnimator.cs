using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimator : MonoBehaviour
{
    [field: SerializeField] public Animator Animator { get; private set; }

    public void SetAnimatorBool(string name, bool value)
    {
        Animator.SetBool(name, value);
    }

    public void SetTrigger(string name)
    {
        Animator.SetTrigger(name);
    }
}