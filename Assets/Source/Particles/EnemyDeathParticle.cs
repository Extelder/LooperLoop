using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticle : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _cooldown;
    private void OnEnable()
    {
        _enemy.DeathEnded += OnDead;
    }

    private void OnDead()
    {
        _particle.Play();
        StartCoroutine(DestroyBody());
    }

    private IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(_cooldown);
        Destroy(gameObject);
    }
    
    private void OnDisable()
    {
        _enemy.DeathEnded -= OnDead;
    }
}
