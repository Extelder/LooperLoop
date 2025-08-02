using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;
    [SerializeField] private Image _staminaBarBG;
    [SerializeField] private PlayerStamina _stamina;
    [SerializeField] private float _speed;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        _stamina.StaminaValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        _stamina.StaminaValueChanged -= OnHealthValueChanged;
        _disposable.Clear();
    }

    public virtual void OnHealthValueChanged(int value)
    {
        _disposable?.Clear();
        float percent = (float) _stamina.MaxValue / 100;
        _staminaBar.fillAmount = value / percent / 100;
        Observable.EveryUpdate().Subscribe(_ =>
        {
            _staminaBarBG.fillAmount = Mathf.Lerp(_staminaBarBG.fillAmount, _staminaBar.fillAmount - 0.05f, _speed);
            if (Mathf.Abs(_staminaBarBG.fillAmount - _staminaBar.fillAmount) <= 0.001)
            {
                _disposable.Clear();
            }
        }).AddTo(_disposable);
    }
}
