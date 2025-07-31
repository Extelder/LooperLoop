using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWalletUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private PlayerWallet _wallet;

    private void OnEnable()
    {
        _wallet.ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _text.text = value.ToString() + "$";
    }

    private void OnDisable()
    {
        _wallet.ValueChanged -= OnValueChanged;
    }
}