using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBuyable : VendingMachineBuyable
{
    public override void OnBought()
    {
        PlayerStaminaCharacteristics.Instance.AddValue(Item.AddibleValue);
    }
}
