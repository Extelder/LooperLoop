using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoneyAdd : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private PlayerWallet _wallet;
    
    private void OnEnable()
    {
        _enemy.Dead += AddMoney;
    }

    public void AddMoney(int value)
    {
        _wallet.Add(value);
    }

    private void OnDisable()
    {
        _enemy.Dead -= AddMoney;
    }
}
