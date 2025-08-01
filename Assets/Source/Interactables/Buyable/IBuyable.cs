using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuyable
{
    public BuyableItem Item { get; set; }
    public bool TryBuy();
}
