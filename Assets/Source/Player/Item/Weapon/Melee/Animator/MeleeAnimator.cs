using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimator : ItemAnimator
{
    [SerializeField] private string _attackBoolName;

    public void AttackAnimation()
    {
        SetAnimatorBool(_attackBoolName, true);
    }

    public void IdleAnimation()
    {
        SetAnimatorBool(_attackBoolName, false);
    }
}