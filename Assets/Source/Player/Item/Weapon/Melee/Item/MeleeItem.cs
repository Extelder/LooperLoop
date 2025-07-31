using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Melee")]
public class MeleeItem : Item
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int Speed { get; private set; }
}