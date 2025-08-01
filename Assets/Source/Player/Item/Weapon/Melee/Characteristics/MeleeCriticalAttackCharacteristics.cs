using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeleeCriticalAttackCharacteristics : PlayerCharacteristic<MeleeCriticalAttackCharacteristics>
{
    [field: SerializeField] public int DamageMultiplier { get; private set; }

    public override void OnValueChanged(int value)
    {
    }
}
