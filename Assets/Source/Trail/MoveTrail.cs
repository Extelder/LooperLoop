using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveTrail : MonoBehaviour
{
    [SerializeField] private int _minSpeed;
    [SerializeField] private int _maxSpeed;
    
    private float _speed;

    private Transform _player;
    private CompositeDisposable _disposable;

    private void OnEnable()
    {
        _player = PlayerCharacter.Instance.Controller.transform;
        _speed = Random.Range(_minSpeed, _maxSpeed);
        StartMove();
    }

    private void StartMove()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
    }
}
