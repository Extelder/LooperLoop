using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public abstract class Attack
{
    [SerializeField] private float _checkRate;
    [SerializeField] private OverlapSettings _checkPlayerOverlapSettings;

    [field: SerializeField] public AnimatorRandomState AttackState { get; private set; }

    [SerializeField] private float _speed;

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
            Collider[] others = new Collider[_checkPlayerOverlapSettings.MaxOverlapColliders];
            Physics.OverlapSphereNonAlloc(_checkPlayerOverlapSettings.Point.position, _checkPlayerOverlapSettings.Range,
                others,
                _checkPlayerOverlapSettings.LayerMask);
            for (int i = 0; i < others.Length; i++)
            {
                if (others[i] == null)
                    continue;
                if (others[i].TryGetComponent<PlayerHealth>(out PlayerHealth PlayerHealth))
                {
                    StartAttackAnim();
                    break;
                }
            }
        }
    }

    public virtual void PerformAttack()
    {
        
    }
    
    public virtual void StartAttackAnim()
    {
        Animator.SetRandomAnimatorBool(AttackState);
    }
}

[Serializable]
public class EnemyAttack
{
    [SerializeField] private EnemyAnimator _animator;

    [SerializeReference] [SerializeReferenceButton]
    private Attack[] _attacks;

    private Attack _currentAttack;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Init()
    {
        int random = UnityEngine.Random.Range(0, _attacks.Length);

        Debug.LogError(random);

        _currentAttack = _attacks[random];
        _currentAttack.Activate(_animator, _disposable);
    }

    public void AttackAnimationFrame()
    {
        _currentAttack
    }
    
    ~EnemyAttack()
    {
        _disposable?.Clear();
    }
}