using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTextureHeightGenerator : MonoBehaviour
{
            [SerializeField] private Terrain _terrain;
    
            [SerializeField] private float _grassHeightThreshold = 0.8f;
            [SerializeField] private float _rockHeightThreshold = 0.5f;
            [SerializeField] private float _sandHeightThreshold = 0.5f;
    
            [SerializeField] private int _grassTextureIndex = 0;
            [SerializeField] private int _rockTextureIndex = 1;
            [SerializeField] private int _sandTextureIndex = 2;
            
            private TerrainData _terrainData;

            public void Generate()
            {
                if (_terrain == null)
                {
                    _terrain = GetComponent<Terrain>();
                }
                _terrainData = _terrain.terrainData;
    
                ApplyTexturesByHeight();
            }
    
            void ApplyTexturesByHeight()
            {
                int alphaMapWidth = _terrainData.alphamapWidth;
                int alphaMapHeight = _terrainData.alphamapHeight;
                int numTextures = _terrainData.terrainLayers.Length;
    
                float[,,] splatmapData = new float[alphaMapWidth, alphaMapHeight, numTextures];
    
                for (int y = 0; y < alphaMapHeight; y++)
                {
                    for (int x = 0; x < alphaMapWidth; x++)
                    {
                        float normalizedHeight = _terrainData.GetHeight(x, y) / _terrainData.size.y;
    
                        for (int i = 0; i < numTextures; i++)
                        {
                            splatmapData[x, y, i] = 0;
                        }
    
                        if (normalizedHeight >= _grassHeightThreshold)
                        {
                            splatmapData[x, y, _grassTextureIndex] = 1;
                        }
                        else if (normalizedHeight >= _rockHeightThreshold)
                        {
                            splatmapData[x, y, _rockTextureIndex] = 1;
                        }
                        else if (normalizedHeight >= _sandHeightThreshold)
                        {
                            splatmapData[x, y, _sandTextureIndex] = 1;
                        }
                        else
                        {
                            //splatmapData[x, y, 2] = 1;
                        }
                    }
                }
    
                _terrainData.SetAlphamaps(0, 0, splatmapData);
            }
}
