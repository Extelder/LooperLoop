using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSpeedCharacteristic : PlayerCharacteristic<MeleeSpeedCharacteristic>
{
    [SerializeField] private MeleeSettings settings;

    [SerializeField] private Animator _animator;

    private int _currentItemSpeedMultiplier = 0;

    private void OnEnable()
    {
        settings.ItemChanged += OnItemChanged;
    }

    private void OnItemChanged(MeleeItem item)
    {
        RemoveValue(_currentItemSpeedMultiplier);
        _currentItemSpeedMultiplier = item.Speed;
        AddValue(_currentItemSpeedMultiplier);
    }

    private void OnDisable()
    {
        settings.ItemChanged -= OnItemChanged;
    }

    public override void OnValueChanged(int value)
    {
        float floatValue = value;
        _animator.speed = floatValue / 4;
    }
}