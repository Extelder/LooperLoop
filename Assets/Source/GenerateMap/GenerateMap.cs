using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MapGenerator.Generators;
using KrisDevelopment.EnvSpawn;
using Unity.AI.Navigation;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private TerrainCollider _terrainCollider;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    [SerializeField] private HeightsGenerator _heightsGenerator;
    [SerializeField] private TreeGenerator _treeGenerator;
    [SerializeField] private TerrainTextureHeightGenerator _terrainTextureHeightGenerator;

    [SerializeField] private EnviroSpawn_CS _enviroSpawn;

    private void Start()
    {
        _terrainCollider.enabled = false;
        _heightsGenerator.Generate();
        _treeGenerator.Generate();
        _terrainTextureHeightGenerator.Generate();
        _terrainCollider.enabled = true;
        EnviroSpawn_CS.MassInstantiateNew();
        _navMeshSurface.BuildNavMesh();
    }
}