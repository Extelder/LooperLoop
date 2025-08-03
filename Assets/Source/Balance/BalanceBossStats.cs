using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBossStats : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private Enemy _enemy;

    [SerializeField] private float _healthScale;
    [SerializeField] private float _rewardPerHP;
    private void Start()
    {
        float bossHp = _enemyHealth.MaxValue * Mathf.Pow(1 + _healthScale, DayCounter.Instance.Current - 1);
        Debug.Log(bossHp);
        _enemy.MoneyValue = (int)(bossHp * _rewardPerHP) * 10;
        Debug.Log(_enemy.MoneyValue);
        _enemyHealth.SetMaxHealth((int)bossHp);
        _enemyHealth.HealToMax();
    }
}
