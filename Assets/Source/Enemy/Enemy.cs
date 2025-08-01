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

    private void Start()
    {
        _enemyChase.Init();
        _enemyAttack.Init();
        _enemyUltimate.Init();

        _enemyChase.StartChase();
        StartCoroutine(_enemyAttack.CurrentAttack.StartChecking());
    }

    public void AttackStarted()
    {
        _enemyChase.StopChase();
    }

    public void PerformAttack()
    {
        _enemyAttack.PerformAttack();
    }

    public void AttackEnded()
    {
        _enemyAttack.StopAttack();
        _enemyChase.StartChase();
    }

    public event Action ChasingEnd;
    public event Action AttackingEnd;
    public event Action UltimateEnd;
    public event Action Death;
}