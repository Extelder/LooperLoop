using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeChangeDropper : MonoBehaviour
{
    [SerializeField] private Transform _dropPoint;
    [SerializeField] private MeleeSettings _meleeSettings;

    private MeleeItem _currentItem;

    private void OnEnable()
    {
        _meleeSettings.ItemChanged += OnItemChanged;
    }

    private void OnItemChanged(MeleeItem newItem)
    {
        if (_currentItem != null)
            DropItem(_currentItem.Prefab);
        _currentItem = newItem;
    }

    public void DropItem(GameObject item)
    {
        Instantiate(item, _dropPoint.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        _meleeSettings.ItemChanged -= OnItemChanged;
    }
}