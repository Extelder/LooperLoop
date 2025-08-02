using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public abstract class Attack
{
    [field: SerializeField] public Material Material { get; private set; }

    [field:SerializeField] protected int Damage { get; private set; }

    [field:SerializeField] protected float CheckRate{ get; private set; }
    [SerializeField] private OverlapSettings _checkPlayerOverlapSettings;

    [field: SerializeField] public AnimatorRandomState AttackState { get; private set; }
    protected EnemyAnimator Animator { get; private set; }
    protected Transform Player { get; private set; }

    private CompositeDisposable _disposable;

    public virtual void Activate(EnemyAnimator animator, CompositeDisposable disposable)
    {
        Animator = animator;
        Player = PlayerCharacter.Instance.Controller.transform;
        _disposable = disposable;
    }

    public virtual IEnumerator StartChecking()
    {
        while (true)
        {
            yield return new WaitForSeconds(CheckRate);
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
                PlayerHitBox.Hit(Damage);
                break;
            }
        }
    }
    
    public virtual void StartAttackAnim()
    {
        Animator.SetRandomAnimatorTrigger(AttackState);
    }
}

[Serializable]
public class HeavyAttack : Attack
{
}

[Serializable]
public class LightAttack : Attack
{
}

[Serializable]
public class MediumAttack : Attack
{
}

[Serializable]
public class ShootAttack : Attack
{
    [SerializeField] private Transform _enemy;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Pool _trailPool;
    
    public override IEnumerator StartChecking()
    {
        while (true)
        {
            yield return new WaitForSeconds(CheckRate);
            if (Physics.Raycast(_enemy.position, Player.position - _enemy.position,
                out RaycastHit hit, _range, _layerMask))
            {
                if (hit.collider.TryGetComponent<PlayerHitBox>(out PlayerHitBox playerHitBox))
                {
                    StartAttackAnim();
                }
            }
        }
    }
    
    public override void PerformAttack()
    {
        if (Physics.Raycast(_enemy.position, Player.position - _enemy.position,
            out RaycastHit hit, _range, _layerMask))
        {
            if (hit.collider.TryGetComponent<PlayerHitBox>(out PlayerHitBox playerHitBox))
            {
                _trailPool.GetFreeElement(_attackPoint.position);
            }
        }
    }
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

    public void Kill()
    {
        CurrentAttack = null;
        _disposable?.Clear();
    }

    public void AttackAnimationFrame()
    {
        CurrentAttack.PerformAttack();
    }

    public void PerformAttack()
    {
        CurrentAttack.PerformAttack();
    }
}