using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : HitBox
{
    [SerializeField] private EnemyHealth _health;

    public override void Hit(int damage)
    {
        _health.TakeDamage(damage);
    }
}