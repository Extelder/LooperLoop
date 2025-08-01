using System;
using System.Collections;
using System.Collections.Generic;
using MilkShake;
using UnityEngine;

public class MeleeAttackShake : MonoBehaviour
{
    [SerializeField] private Shaker _shaker;
    
    [SerializeField] private ShakePreset _attackShake;
    [SerializeField] private ShakePreset _hitShake;

    [SerializeField] private MeleeAttack _attack;

    private void OnEnable()
    {
        _attack.Attacked += OnAttacked;
        _attack.Hitted += OnHitted;
    }

    private void OnHitted()
    {
        _shaker.Shake(_hitShake);
    }

    private void OnAttacked()
    {
        _shaker.Shake(_attackShake);
    }

    private void OnDisable()
    {
        _attack.Attacked -= OnAttacked;
        _attack.Hitted -= OnHitted;
    }
}