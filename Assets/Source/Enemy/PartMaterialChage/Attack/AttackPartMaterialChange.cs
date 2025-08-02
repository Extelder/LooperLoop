using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPartMaterialChange : PartMaterialChange
{
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _enemy.AttackBootstrapped += OnAttackBootstrapped;
    }

    private void OnAttackBootstrapped(Attack attack)
    {
        Change(attack.Material);        
    }

    private void OnDisable()
    {
        _enemy.AttackBootstrapped -= OnAttackBootstrapped;
    }
}
