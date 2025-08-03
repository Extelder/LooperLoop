using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;

    private void OnEnable()
    {
        Enemy.Killed += OnEnemyKilled;
        Enemy.Spawned += OnEnemySpawned;
    }

    private void OnEnemySpawned()
    {
        _musicSource.enabled = false;
    }

    private void OnEnemyKilled()
    {
        _musicSource.enabled = true;
    }

    private void OnDisable()
    {
        Enemy.Killed -= OnEnemyKilled;
        Enemy.Spawned -= OnEnemySpawned;
    }
}
