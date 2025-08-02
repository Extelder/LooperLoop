using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartMaterialChangable : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private Material[] _materials;

    private void OnEnable()
    {
        _enemy.Bootstrapped += OnEnemyBootstrapped;
    }

    private void OnEnemyBootstrapped()
    {
        _meshRenderer.materials = _materials;
    }

    private void OnDisable()
    {
        _enemy.Bootstrapped -= OnEnemyBootstrapped;
    }

    public void AddMaterials(Material[] materials, int[] indexies)
    {
        if (_materials == null)
            _materials = _meshRenderer.materials;
        for (int i = 0; i < indexies.Length; i++)
        {
            _materials[indexies[i]] = materials[i];
        }
    }
}