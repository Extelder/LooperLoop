using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action ChasingEnd;
    public event Action AttackingEnd;
    public event Action UltimateEnd;
    public event Action Death;
}