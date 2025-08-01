using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForceBuyable : VendingMachineBuyable
{
    public override void OnBought()
    {
        JumpCharacteristic.Instance.AddValue(Item.AddibleValue);
    }
}
