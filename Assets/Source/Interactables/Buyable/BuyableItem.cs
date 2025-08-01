using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buy")]
public class BuyableItem : ScriptableObject
{
    [field: SerializeField] public int Price { get; set; }
    [field: SerializeField] public int AddibleValue { get; set; }
}
