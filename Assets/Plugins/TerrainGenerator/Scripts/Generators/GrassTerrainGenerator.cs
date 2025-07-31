using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrassTerrainGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D _grassTexture;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private Color _color;

    private List<DetailPrototype> _prototypes;
    private DetailPrototype _grassPrototype;
    
    private void Start()
    {
        DetailPrototype grassPrototype = new DetailPrototype();
        grassPrototype.prototypeTexture = _grassTexture;
        grassPrototype.minWidth = 0.5f; 
        grassPrototype.maxWidth = 1.0f; 
        grassPrototype.minHeight = 0.5f; 
        grassPrototype.maxHeight = 1.0f; 
        grassPrototype.renderMode = DetailRenderMode.Grass;
        grassPrototype.dryColor = _color;
        grassPrototype.healthyColor = _color;
        TerrainData terrainData = _terrain.terrainData;
        _prototypes = new List<DetailPrototype>(terrainData.detailPrototypes);
        _prototypes.Add(grassPrototype); 
        terrainData.detailPrototypes = _prototypes.ToArray();
        int detailResolution = 2048; 
        terrainData.SetDetailResolution(detailResolution, 8);
        int[,] detailMap = new int[detailResolution, detailResolution];
        for (int y = 0; y < detailResolution; y++)
        {
            for (int x = 0; x < detailResolution; x++)
            {
                detailMap[x, y] = 1;
            }
        }
        terrainData.SetDetailLayer(0, 0, 0, detailMap);
    }

    private void OnDisable()
    {
        _terrain.terrainData.detailPrototypes = new DetailPrototype[0];
    }
}
