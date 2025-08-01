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

    private void Start()
    {
        _enemyChase.Init();
    }

    public event Action ChasingEnd;
    public event Action AttackingEnd;
    public event Action UltimateEnd;
    public event Action Death;
}