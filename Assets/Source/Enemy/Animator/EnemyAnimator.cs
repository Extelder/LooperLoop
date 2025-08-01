using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AnimatorRandomState
{
    [field: SerializeField] public int Index { get; private set; }
    [field: SerializeField] public int IntName { get; private set; }
    [field: SerializeField] public string TriggerName { get; private set; }
}

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Chase(int index)
    {
    }

    public void Attack()
    {
    }

    public void Ultimate()
    {
    }

    public void Death()
    {
    }

    public void SetAnimationBool()
    {
    }

    public void SetRandomAnimatorBool(AnimatorRandomState animatorRandomState)
    {
        _animator.SetTrigger(animatorRandomState.TriggerName);
        _animator.SetInteger(animatorRandomState.IntName, animatorRandomState.Index);
    }

    public void SetRandomAnimatorBoolAndDisableOthers(AnimatorRandomState animatorRandomState)
    {
        _animator.SetTrigger(animatorRandomState.TriggerName);
        _animator.SetInteger(animatorRandomState.IntName, animatorRandomState.Index);
    }
}