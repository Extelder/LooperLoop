using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public abstract class Attack
{
    [SerializeField] private int _damage;

    [SerializeField] private float _checkRate;
    [SerializeField] private OverlapSettings _checkPlayerOverlapSettings;

    [field: SerializeField] public AnimatorRandomState AttackState { get; private set; }
    [field: SerializeField] public AnimatorRandomState AttackStopState { get; private set; }

    protected EnemyAnimator Animator { get; private set; }
    protected Transform Player { get; private set; }

    private CompositeDisposable _disposable;

    public virtual void Activate(EnemyAnimator animator, CompositeDisposable disposable)
    {
        Animator = animator;
        Player = PlayerCharacter.Instance.Controller.transform;
        _disposable = disposable;
    }

    public IEnumerator StartChecking()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkRate);
            Debug.LogError("Checking");
            Collider[] others = new Collider[_checkPlayerOverlapSettings.MaxOverlapColliders];
            Physics.OverlapSphereNonAlloc(_checkPlayerOverlapSettings.Point.position, _checkPlayerOverlapSettings.Range,
                others,
                _checkPlayerOverlapSettings.LayerMask);
            for (int i = 0; i < others.Length; i++)
            {
                if (others[i] == null)
                    continue;
                if (others[i].TryGetComponent<PlayerHitBox>(out PlayerHitBox PlayerHitBox))
                {
                    StartAttackAnim();
                    break;
                }
            }
        }
    }

    public void StopAttack()
    {
        Animator.SetRandomAnimatorTrigger(AttackStopState);
    }

    public virtual void PerformAttack()
    {
        Collider[] others = new Collider[_checkPlayerOverlapSettings.MaxOverlapColliders];
        Physics.OverlapSphereNonAlloc(_checkPlayerOverlapSettings.Point.position, _checkPlayerOverlapSettings.Range,
            others,
            _checkPlayerOverlapSettings.LayerMask);
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i] == null)
                continue;
            if (others[i].TryGetComponent<PlayerHitBox>(out PlayerHitBox PlayerHitBox))
            {
                PlayerHitBox.Hit(_damage);
                break;
            }
        }
    }

    public virtual void StartAttackAnim()
    {
        Animator.SetRandomAnimatorTrigger(AttackState);
    }
}

public class HeavyAttack : Attack
{
}

public class LightAttack : Attack
{
}

[Serializable]
public class EnemyAttack
{
    [SerializeField] private EnemyAnimator _animator;

    [SerializeReference] [SerializeReferenceButton]
    private Attack[] _attacks;

    public Attack CurrentAttack { get; private set; }

    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Init()
    {
        int random = UnityEngine.Random.Range(0, _attacks.Length);

        Debug.LogError(random);

        CurrentAttack = _attacks[random];
        CurrentAttack.Activate(_animator, _disposable);
    }

    public void AttackAnimationFrame()
    {
        CurrentAttack.PerformAttack();
    }

    public void PerformAttack()
    {
        CurrentAttack.PerformAttack();
    }

    public void StopAttack()
    {
        CurrentAttack.StopAttack();
    }

    ~EnemyAttack()
    {
        _disposable?.Clear();
    }
}