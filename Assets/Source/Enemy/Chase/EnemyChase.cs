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
    [field: SerializeField] public Material Material;
    [field: SerializeField] public AnimatorRandomState ChaseState { get; private set; }

    [SerializeField] private float _speed;

    protected NavMeshAgent Agent { get; private set; }
    protected EnemyAnimator Animator { get; private set; }
    protected Transform Player { get; private set; }

    protected CompositeDisposable _disposable = new CompositeDisposable();

    public virtual void Activate(NavMeshAgent agent, EnemyAnimator animator)
    {
        Agent = agent;
        Animator = animator;
        Player = PlayerCharacter.Instance.Controller.transform;
        Agent.speed = _speed;
    }

    public abstract void StartChase();
    public abstract void StopChase();

    public void DestinationToPlayer()
    {
        Observable.EveryUpdate().Subscribe(_ => { Agent.SetDestination(Player.position); }).AddTo(_disposable);
    }

    ~Chase()
    {
        _disposable?.Clear();
    }
}

[Serializable]
public class BaseChase : Chase
{
    [SerializeField] private AnimatorRandomState _chaseEndState;

    public override void StartChase()
    {
        Debug.LogError("Stop");
        Agent.isStopped = false;
        DestinationToPlayer();
        Animator.SetRandomAnimatorTrigger(ChaseState);
    }

    public override void StopChase()
    {
        Debug.LogError("Start");
        _disposable?.Clear();
        Agent.isStopped = true;
        Animator.SetRandomAnimatorTrigger(_chaseEndState);
    }
}

[Serializable]
public class FlyChase : Chase
{
    public override void StartChase()
    {
        DestinationToPlayer();
        Animator.SetRandomAnimatorTrigger(ChaseState);
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

    public Chase CurrentChase { get; private set; }

    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Init()
    {
        int random = UnityEngine.Random.Range(0, _chases.Length);

        Debug.LogError(random);

        CurrentChase = _chases[random];
        CurrentChase.Activate(_agent, _animator);
    }

    ~EnemyChase()
    {
        _disposable.Clear();
        CurrentChase.StopChase();
        CurrentChase = null;
    }

    public void StartChase()
    {
        CurrentChase.StartChase();
    }

    public void StopChase()
    {
        CurrentChase.StopChase();
    }
}