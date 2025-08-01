using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AnimatorRandomState
{
    [field: SerializeField] public int Index { get; private set; }
    [field: SerializeField] public string IntName { get; private set; }
    [field: SerializeField] public string TriggerName { get; private set; }
}

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private List<string> _triggers = new List<string>();

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

    public void SetRandomAnimatorTrigger(AnimatorRandomState animatorRandomState)
    {
        if (!_triggers.Contains(animatorRandomState.TriggerName))
            _triggers.Add(animatorRandomState.TriggerName);

        _triggers.ForEach(_ => { _animator.ResetTrigger(_); });

        _animator.SetInteger(animatorRandomState.IntName, animatorRandomState.Index);
        _animator.SetTrigger(animatorRandomState.TriggerName);
    }
}