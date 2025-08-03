using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDeathItemDrop : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private MeleeItem[] _items;
    [SerializeField] private float _chance;
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _enemy.DeathEnded += OnDeathEnded;
    }

    private void OnDeathEnded()
    {
        if (Random.value <= _chance)
        {
            Instantiate(_items[Random.Range(0, _items.Length)].Prefab, _point.position, Quaternion.identity);
        }
    }

    private void OnDisable()
    {
        _enemy.DeathEnded -= OnDeathEnded;
    }
}