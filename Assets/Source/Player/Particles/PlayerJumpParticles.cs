using System;
using System.Collections;
using System.Collections.Generic;
using EvolveGames;
using UniRx;
using UnityEngine;

public class PlayerJumpParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private PlayerController _controller;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        _controller.Jumped += OnJumped;
    }

    private void OnJumped()
    {
        _disposable?.Clear();
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_controller.characterController.isGrounded)
            {
                _particle.Play();
                _disposable.Clear();
            }
        }).AddTo(_disposable);
        _particle.Play();
    }

    private void OnDisable()
    {
        _disposable?.Clear();
        _controller.Jumped -= OnJumped;
    }
}