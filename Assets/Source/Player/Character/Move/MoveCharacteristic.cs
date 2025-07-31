using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class MoveCharacteristic : PlayerCharacteristic<MoveCharacteristic>
{
    public override void OnValueChanged(int value)
    {
        Character.Controller.WalkingValue = value;
        Character.Controller.RuningSpeed = value * 1.5f;
    }
}