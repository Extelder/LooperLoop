using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.HealthValueChanged += OnHealthValueChanged;
        _health.MaxHealthValueChanged += OnMaxHealthValueChanged;
    }

    private void OnMaxHealthValueChanged(int value)
    {
        UpdateText();
    }

    private void OnHealthValueChanged(int value)
    {
        UpdateText();
    }

    public void UpdateText()
    {
        _text.text = _health.CurrentValue + "/" + _health.MaxValue;
    }

    private void OnDisable()
    {
        _health.HealthValueChanged -= OnHealthValueChanged;
        _health.MaxHealthValueChanged -= OnMaxHealthValueChanged;
    }
}