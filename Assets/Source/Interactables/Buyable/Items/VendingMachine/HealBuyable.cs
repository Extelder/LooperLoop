using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBuyable : VendingMachineBuyable
{
    public override void OnBought()
    {
        PlayerHealthCharacteristic.Instance.Heal(Item.AddibleValue);
    }
}
