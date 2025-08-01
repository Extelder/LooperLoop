using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedBuyable : VendingMachineBuyable
{
    public override void OnBought()
    {
        MeleeSpeedCharacteristic.Instance.AddValue(Item.AddibleValue);
    }
}
