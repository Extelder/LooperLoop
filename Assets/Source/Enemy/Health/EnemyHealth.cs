using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private void Start()
    {
        HealToMax();
    }

    public override void Death()
    {
    }
}