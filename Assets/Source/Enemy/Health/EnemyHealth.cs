using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public event Action<int> HealthBootstraped;
    private void Start()
    {
        HealToMax();
        HealthBootstraped?.Invoke(MaxValue);
    }

    public override void Death()
    {
    }
}