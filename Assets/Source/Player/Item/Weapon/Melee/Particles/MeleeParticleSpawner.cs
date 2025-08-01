using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeParticleSpawner : MonoBehaviour
{
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private Pool _objectPool;
    [SerializeField] private Pool _meatPool;

    private void OnEnable()
    {
        _meleeAttack.RaycastHitted += OnRaycastHitted;
    }

    private void OnRaycastHitted(RaycastHit hit, HitBox hitBox)
    {
        if (hitBox as ObjectHitBox)
        {
            _objectPool.GetFreeElement(hit.point, hit.normal);
        }
        else
        {
            _meatPool.GetFreeElement(hit.point);
        }
    }

    private void OnDisable()
    {
        _meleeAttack.RaycastHitted -= OnRaycastHitted;
    }
}
