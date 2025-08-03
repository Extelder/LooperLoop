using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    [SerializeField] private float _force;
    private Transform _player;
    private Rigidbody _rigidbody;
    
    private void OnEnable()
    {
        _player = PlayerCharacter.Instance.Controller.transform;
        _rigidbody = GetComponent<Rigidbody>();
        Throw();
    }

    private void Throw()
    {
        _rigidbody.AddForce(Vector3.up, ForceMode.Impulse);
        _rigidbody.AddForce(Vector3.Normalize(_player.position - transform.position) * _force, ForceMode.Impulse);
    }
}
