using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePartMaterialChange : PartMaterialChange
{
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _enemy.ChaseBootstrapped += OnChaseBootstrapped;
    }

    private void OnChaseBootstrapped(Chase chase)
    {
        Change(chase.Material);        
    }

    private void OnDisable()
    {
        _enemy.ChaseBootstrapped -= OnChaseBootstrapped;
    }
}
