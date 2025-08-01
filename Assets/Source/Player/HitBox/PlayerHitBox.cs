using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] private PlayerHealth _health;

    public void Hit(int damage)
    {
        _health.TakeDamage(damage);
    }
}