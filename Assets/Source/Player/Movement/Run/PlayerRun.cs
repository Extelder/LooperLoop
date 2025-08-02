using System;
using System.Collections;
using System.Collections.Generic;
using EvolveGames;
using UniRx;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private PlayerStamina _stamina;
    [SerializeField] private int _staminaValue;
    [SerializeField] private double _wasteRate;
    [SerializeField] private float _recoverRate;
    [SerializeField] private float _recoverDelay;

    private CompositeDisposable _disposable = new CompositeDisposable();
    private CompositeDisposable _wasteDisposable = new CompositeDisposable();
    private CompositeDisposable _recoverDisposable = new CompositeDisposable();
    
    private void OnEnable()
    {
        _controller.isRunning.Subscribe(value =>
        {
            if (value)
            {
                StopAllCoroutines();
                _recoverDisposable?.Clear();
                Observable.Interval(TimeSpan.FromSeconds(_wasteRate)).Subscribe(_ =>
                {
                    if (_stamina.CurrentValue <= 0)
                    {
                        _controller.CanRunning = false;
                    }
                    _stamina.Waste(_staminaValue);
                }).AddTo(_wasteDisposable);
            }
            else
            {
                StartCoroutine(Recover());
            }
        }).AddTo(_disposable);
    }

    private IEnumerator Recover()
    {
        _wasteDisposable.Clear();
        yield return new WaitForSeconds(_recoverDelay);
        Observable.Interval(TimeSpan.FromSeconds(_recoverRate)).Subscribe(_ =>
        {
            _stamina.Recover(_staminaValue);
            if (_stamina.CurrentValue > 0)
            {
                _controller.CanRunning = true;
            }
        }).AddTo(_recoverDisposable);
    }

    private void OnDisable()
    {
        _recoverDisposable.Clear();
        _wasteDisposable.Clear();
        _disposable.Clear();
    }
}
