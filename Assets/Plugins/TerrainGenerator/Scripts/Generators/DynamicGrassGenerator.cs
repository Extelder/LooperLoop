    using UnityEngine;

    public class DynamicGrassGenerator : MonoBehaviour
    {
        [SerializeField] private Terrain _terrain;
        [SerializeField] private Transform _player;
        [SerializeField] private float _grassRadius = 20f; // Radius around player to generate grass
        [SerializeField] private int _detailLayerIndex = 0; // Index of your grass detail layer

        private int[,] _detailMap;
        private int _detailResolution;

        void Start()
        {
            if (_terrain == null || _player == null)
            {
                Debug.LogError("Terrain or Player not assigned!");
                enabled = false;
                return;
            }

            _detailResolution = _terrain.terrainData.detailResolution;
            _detailMap = new int[_detailResolution, _detailResolution];
        }

        void Update()
        {
            UpdateGrassAroundPlayer();
        }

        void UpdateGrassAroundPlayer()
        {
            // Clear previous grass data (optional, for a clean update)
            for (int i = 0; i < _detailResolution; i++)
            {
                for (int j = 0; j < _detailResolution; j++)
                {
                    _detailMap[i, j] = 0; // Set to 0 for no grass
                }
            }

            // Convert player's world position to terrain coordinates
            Vector3 playerTerrainPos = _terrain.transform.InverseTransformPoint(_player.position);
            float normalizedX = playerTerrainPos.x / _terrain.terrainData.size.x;
            float normalizedZ = playerTerrainPos.z / _terrain.terrainData.size.z;

            int playerDetailX = Mathf.FloorToInt(normalizedX * _detailResolution);
            int playerDetailZ = Mathf.FloorToInt(normalizedZ * _detailResolution);

            // Iterate through a square area around the player in detail map coordinates
            int minX = Mathf.Max(0, playerDetailX - Mathf.CeilToInt(_grassRadius / _terrain.terrainData.size.x * _detailResolution));
            int maxX = Mathf.Min(_detailResolution - 1, playerDetailX + Mathf.CeilToInt(_grassRadius / _terrain.terrainData.size.x * _detailResolution));
            int minZ = Mathf.Max(0, playerDetailZ - Mathf.CeilToInt(_grassRadius / _terrain.terrainData.size.z * _detailResolution));
            int maxZ = Mathf.Min(_detailResolution - 1, playerDetailZ + Mathf.CeilToInt(_grassRadius / _terrain.terrainData.size.z * _detailResolution));

            for (int x = minX; x <= maxX; x++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    // Convert detail map coordinates back to world position for distance check
                    Vector3 worldPos = new Vector3(
                        (float)x / _detailResolution * _terrain.terrainData.size.x,
                        0, // Y coordinate is not relevant for horizontal distance
                        (float)z / _detailResolution * _terrain.terrainData.size.z
                    ) + _terrain.transform.position;

                    if (Vector3.Distance(worldPos, _player.position) <= _grassRadius)
                    {
                        _detailMap[z, x] = 15; // Set to max density (15) for grass
                    }
                }
            }

            _terrain.terrainData.SetDetailLayer(0, 0, _detailLayerIndex, _detailMap);
        }
    }
