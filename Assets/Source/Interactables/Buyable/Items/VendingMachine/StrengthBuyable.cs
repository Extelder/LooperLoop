using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthBuyable : VendingMachineBuyable
{
    public override void OnBought()
    {
        MeleeDamageCharacteristic.Instance.AddValue(Item.AddibleValue);
    }
}
