using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCharacteristic : PlayerCharacteristic<JumpCharacteristic>
{
    public override void OnValueChanged(int value)
    {
        Character.Controller.JumpSpeed = value;
    }
}