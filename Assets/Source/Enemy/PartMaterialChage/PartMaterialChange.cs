using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PartMaterialChange : MonoBehaviour
{
    [SerializeField] private PartMaterialChangable _materialChangable;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    [SerializeField] private int[] _materialIndexes;


    public void Change(Material material)
    {
        Material[] materials = new Material[_materialIndexes.Length];

        for (int i = 0; i < _materialIndexes.Length; i++)
        {
            materials[i] = material;
        }

        _materialChangable.AddMaterials(materials, _materialIndexes);
    }
}