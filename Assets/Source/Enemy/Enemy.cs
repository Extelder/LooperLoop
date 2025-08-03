using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyChase _enemyChase;
    [Space(20)] [SerializeField] private EnemyAttack _enemyAttack;
    [Space(20)] [SerializeField] private EnemyUltimate _enemyUltimate;
    [Space(20)] [SerializeField] private EnemyDeath _enemyDeath;

    public event Action Bootstrapped;
    public event Action<Chase> ChaseBootstrapped;
    public event Action<Attack> AttackBootstrapped;
    public event Action<Ultimate> UltimateBootstrapped;

    public static event Action Killed;

    private bool _active = true;

    private void Start()
    {
        _enemyChase.Init();
        ChaseBootstrapped?.Invoke(_enemyChase.CurrentChase);
        _enemyAttack.Init();
        AttackBootstrapped?.Invoke(_enemyAttack.CurrentAttack);
        _enemyUltimate.Init();
        UltimateBootstrapped?.Invoke(_enemyUltimate.CurrentUltimate);
        _enemyDeath.Init(this);


        _enemyChase.StartChase();
        StartCoroutine(_enemyAttack.CurrentAttack.StartChecking());
        Bootstrapped?.Invoke();
    }

    public void Kill()
    {
        _active = false;
        _enemyChase.Kill();
        _enemyAttack.Kill();
        _enemyUltimate.Kill();
        Killed?.Invoke();
    }

    public void AttackStarted()
    {
        if (_active)
            _enemyChase.StopChase();
    }

    public void PerformAttack()
    {
        if (_active)
            _enemyAttack.PerformAttack();
    }

    public void AttackEnded()
    {
        if (!_active)
            return;
        _enemyChase.StartChase();
    }

    public event Action ChasingEnd;
    public event Action AttackingEnd;
    public event Action UltimateEnd;
    public event Action Death;
}