using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDamageBuyable : VendingMachineBuyable
{
    public override void OnBought()
    {
        MeleeCriticalAttackCharacteristics.Instance.AddValue(Item.AddibleValue);
    }
}
