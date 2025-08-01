using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedBuyable : VendingMachineBuyable
{
    public override void OnBought()
    {
        MoveCharacteristic.Instance.AddValue(Item.AddibleValue);
    }
}
