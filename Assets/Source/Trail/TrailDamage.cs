using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDamage : MonoBehaviour
{
    [SerializeField] private int _damage;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHitBox>(out PlayerHitBox hitBox))
        {
            hitBox.Hit(_damage);
            gameObject.SetActive(false);
        }
    }
}
