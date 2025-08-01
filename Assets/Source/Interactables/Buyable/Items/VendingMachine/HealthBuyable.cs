using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuyable : VendingMachineBuyable
{
    public override void OnBought()
    {
        PlayerHealthCharacteristic.Instance.AddValue(Item.AddibleValue);
    }
}
