using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartMaterialChangable : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private Material[] _materials;

    private void Start()
    {
        _meshRenderer.materials = _materials;
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