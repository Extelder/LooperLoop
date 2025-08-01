using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healtBar;
    [SerializeField] private Image _healthBarBG;
    [SerializeField] private Health _health;
    [SerializeField] private float _speed;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        _health.HealthValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        _health.HealthValueChanged -= OnHealthValueChanged;
        _disposable.Clear();
    }

    public virtual void OnHealthValueChanged(int value)
    {
        _disposable?.Clear();
        float percent = (float) _health.MaxValue / 100;
        _healtBar.fillAmount = value / percent / 100;
        Observable.EveryUpdate().Subscribe(_ =>
        {
            _healthBarBG.fillAmount = Mathf.Lerp(_healthBarBG.fillAmount, _healtBar.fillAmount, _speed);
            if (Mathf.Abs(_healthBarBG.fillAmount - _healtBar.fillAmount) <= 0.001)
            {
                _disposable.Clear();
            }
        }).AddTo(_disposable);
    }
}