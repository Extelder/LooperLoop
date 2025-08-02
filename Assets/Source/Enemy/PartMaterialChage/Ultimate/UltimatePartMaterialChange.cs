using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimatePartMaterialChange : PartMaterialChange
{
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _enemy.UltimateBootstrapped += OnUltimateBootstrapped;
    }

    private void OnUltimateBootstrapped(Ultimate ultimate)
    {
        Change(ultimate.Material);
    }

    private void OnDisable()
    {
        _enemy.UltimateBootstrapped -= OnUltimateBootstrapped;
    }
}