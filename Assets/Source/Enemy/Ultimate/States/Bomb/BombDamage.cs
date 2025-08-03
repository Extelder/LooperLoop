using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    [SerializeField] private OverlapSettings _checkPlayerOverlapSettings;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _bombDamage;
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _particle;

    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(StartExplode());
        _collider.enabled = false;
    }

    private IEnumerator StartExplode()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(_cooldown);
        Collider[] others = new Collider[_checkPlayerOverlapSettings.MaxOverlapColliders];
        Physics.OverlapSphereNonAlloc(_checkPlayerOverlapSettings.Point.position, _checkPlayerOverlapSettings.Range,
            others,
            _checkPlayerOverlapSettings.LayerMask);
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i] == null)
                continue;
            if (others[i].TryGetComponent<PlayerHitBox>(out PlayerHitBox playerHitBox))
            {
                playerHitBox.Hit(_bombDamage);
            }
        }
        _particle.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _collider.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_checkPlayerOverlapSettings.Point.position, _checkPlayerOverlapSettings.Range);
    }
}
