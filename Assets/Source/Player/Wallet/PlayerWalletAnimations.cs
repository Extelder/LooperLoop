using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWalletAnimations : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _boolName;

    [SerializeField] private float _coolDown;

    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Color _gainColor;
    [SerializeField] private Color _spendColor;
    
    private void OnEnable()
    {
        _playerWallet.MoneyChanged += OnMoneyChanged;
    }

    private void OnMoneyChanged(int value)
    {
        StartCoroutine(RevertChanged());
        if (value >= 0)
        {
            _valueText.color = _gainColor;
            _valueText.text = "+" + value + "$";
            _animator.SetBool(_boolName, true);
        }
        else
        {
            _valueText.color = _spendColor;
            _valueText.text = value + "$";
            _animator.SetBool(_boolName, true);
        }
    }

    private IEnumerator RevertChanged()
    {
        _valueText.gameObject?.SetActive(false);
        _valueText.gameObject.SetActive(true);
        yield return new WaitForSeconds(_coolDown);
        _valueText.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _playerWallet.MoneyChanged -= OnMoneyChanged;
    }
}
