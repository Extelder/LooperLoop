using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PartMaterialChange : MonoBehaviour
{
    [SerializeField] private PartMaterialChangable _materialChangable;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    [SerializeField] private Material _material;

    [SerializeField] private int[] _materialIndexes;


    private void Awake()
    {
        Change();
    }

    public void Change()
    {
        Material[] materials = new Material[_materialIndexes.Length];

        for (int i = 0; i < _materialIndexes.Length; i++)
        {
            materials[i] = _material;
        }

        _materialChangable.AddMaterials(materials, _materialIndexes);
    }
}