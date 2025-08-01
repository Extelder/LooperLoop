using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public abstract class Chase
{
    [field: SerializeField] public AnimatorRandomState ChaseState { get; private set; }

    [SerializeField] private float _speed;

    protected NavMeshAgent Agent { get; private set; }
    protected EnemyAnimator Animator { get; private set; }
    protected Transform Player { get; private set; }

    private CompositeDisposable _disposable;

    public virtual void Activate(NavMeshAgent agent, EnemyAnimator animator, CompositeDisposable disposable)
    {
        Agent = agent;
        Animator = animator;
        Player = PlayerCharacter.Instance.Controller.transform;
        _disposable = disposable;
        Agent.speed = _speed;
    }

    public abstract void StartChase();
    public abstract void StopChase();

    public void DestinationToPlayer()
    {
        Observable.EveryUpdate().Subscribe(_ => { Agent.SetDestination(Player.position); }).AddTo(_disposable);
    }
}

[Serializable]
public class BaseChase : Chase
{
    public override void StartChase()
    {
        Agent.isStopped = false;
        DestinationToPlayer();
        Animator.SetRandomAnimatorBool(ChaseState);
    }

    public override void StopChase()
    {
        Agent.isStopped = true;
    }
}

[Serializable]
public class FlyChase : Chase
{
    public override void StartChase()
    {
        Agent.isStopped = false;
        DestinationToPlayer();
        Animator.SetRandomAnimatorBool(ChaseState);
    }

    public override void StopChase()
    {
    }
}

[Serializable]
public class EnemyChase
{
    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private EnemyAnimator _animator;

    [SerializeReference] [SerializeReferenceButton]
    private Chase[] _chases;

    private Chase _currentChase;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Init()
    {
        int random = UnityEngine.Random.Range(0, _chases.Length);

        Debug.LogError(random);

        _currentChase = _chases[random];
        _currentChase.Activate(_agent, _animator, _disposable);
    }

    public void StartChase()
    {
        _currentChase.StartChase();
    }

    ~EnemyChase()
    {
        _disposable?.Clear();
    }
}