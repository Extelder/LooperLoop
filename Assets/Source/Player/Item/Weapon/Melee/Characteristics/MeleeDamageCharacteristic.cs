using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageCharacteristic : PlayerCharacteristic<MeleeDamageCharacteristic>
{
    [SerializeField] private MeleeSettings _settings;

    private MeleeItem _currentItem;

    private void OnEnable()
    {
        _settings.ItemChanged += OnItemChanged;
    }

    private void OnItemChanged(MeleeItem item)
    {
        if (_currentItem != null)
            RemoveValue(_currentItem.Damage);
        _currentItem = item;
        AddValue(_currentItem.Damage);
    }

    private void OnDisable()
    {
        _settings.ItemChanged -= OnItemChanged;
    }

    public override void OnValueChanged(int value)
    {
    }
}