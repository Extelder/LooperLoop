using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoneyAdd : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _enemy.Dead += AddMoney;
    }

    public void AddMoney(int value)
    {
        PlayerWallet.Instance.Add(value);
    }

    private void OnDisable()
    {
        _enemy.Dead -= AddMoney;
    }
}