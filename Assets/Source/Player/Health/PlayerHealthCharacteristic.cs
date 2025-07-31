using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthCharacteristic : PlayerCharacteristic<PlayerHealthCharacteristic>
{
    [SerializeField] private PlayerHealth _health;

    public override void OnValueChanged(int value)
    {
        _health.SetMaxHealth(value);
    }

    public override void Generate()
    {
        base.Generate();
        _health.SetMaxHealth(CurrentValue);
        _health.HealToMax();
        OnValueChanged(CurrentValue);
    }
}