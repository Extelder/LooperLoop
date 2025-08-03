using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackSound : MonoBehaviour
{
    [SerializeField] private MeleeAttack _attack;
    [SerializeField] private AudioSource _attackSound;

    private void OnEnable()
    {
        _attack.Attacked += OnAttacked;
    }

    private void OnAttacked()
    {
        _attackSound.Play();
    }

    private void OnDisable()
    {
        _attack.Attacked -= OnAttacked;
    }
}