using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public abstract class Death
{
    [field: SerializeField] public AnimatorRandomState DeathState { get; private set; }
    protected EnemyAnimator Animator { get; private set; }
    protected Enemy Enemy { get; private set; }
    protected EnemyHealth Health { get; private set; }

    public virtual void Activate(Enemy enemy, EnemyAnimator enemyAnimator, EnemyHealth health)
    {
        Enemy = enemy;
        Animator = enemyAnimator;
        Health = health;
        Health.Dead += OnDead;
    }

    private void OnDead()
    {
        Debug.LogError("DEATH");
        PerformDeath();
    }

    public abstract void PerformDeath();

    ~Death()
    {
        Health.Dead -= OnDead;
    }
}

[Serializable]
public class AnimateDeath : Death
{
    public override void PerformDeath()
    {
        Enemy.Kill();
        Animator.SetRandomAnimatorTrigger(DeathState);
    }
}

[Serializable]
public class EnemyDeath
{
    [SerializeField] private EnemyHealth _health;

    [SerializeField] private EnemyAnimator _animator;

    [SerializeReference] [SerializeReferenceButton]
    private Death[] _deaths;

    public Death CurrentDeath { get; private set; }

    public void Init(Enemy enemy)
    {
        CurrentDeath = _deaths[Random.Range(0, _deaths.Length)];
        CurrentDeath.Activate(enemy, _animator, _health);
    }
}