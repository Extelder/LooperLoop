using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaCharacteristics : PlayerCharacteristic<PlayerStaminaCharacteristics>
{
    [SerializeField] private PlayerStamina _stamina;
    public override void OnValueChanged(int value)
    {
        _stamina.SetMaxStamina(value);
    }
    
    public override void Generate()
    {
        base.Generate();
        _stamina.SetMaxStamina(CurrentValue);
        _stamina.RecoverToMax();
        OnValueChanged(CurrentValue);
    }
}
