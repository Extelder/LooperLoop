using System;
using System.Collections;
using System.Collections.Generic;
using EvolveGames;
using UniRx;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _moveAnimationBool;
    [SerializeField] private PlayerController _controller;

    private CompositeDisposable _waitForGroundedDisposable = new CompositeDisposable();

    private void OnEnable()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_controller.characterController.isGrounded && _controller.Moving)
            {
                _animator.SetBool(_moveAnimationBool, true);
            }
            else
            {
                _animator.SetBool(_moveAnimationBool, false);
            }
        }).AddTo(_waitForGroundedDisposable);
    }

    private void OnDisable()
    {
        _waitForGroundedDisposable?.Clear();
    }
}