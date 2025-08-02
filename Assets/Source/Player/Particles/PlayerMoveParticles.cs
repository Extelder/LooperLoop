using System;
using System.Collections;
using System.Collections.Generic;
using EvolveGames;
using UniRx;
using UnityEngine;

public class PlayerMoveParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private PlayerController _controller;

    private CompositeDisposable _disposable = new CompositeDisposable();
    private CompositeDisposable _waitForGroundedDisposable = new CompositeDisposable();

    private void OnEnable()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_controller.characterController.isGrounded && _controller.Moving)
            {
                _particle.Play();
            }
            else
            {
                _particle.Stop();
            }
        }).AddTo(_waitForGroundedDisposable);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
        _waitForGroundedDisposable?.Clear();
    }
}