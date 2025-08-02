using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStaminaBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private PlayerStamina _stamina;

    private void OnEnable()
    {
        _stamina.StaminaValueChanged += OnHealthValueChanged;
        _stamina.MaxStaminaValueChanged += OnMaxHealthValueChanged;
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
        _text.text = _stamina.CurrentValue + "/" + _stamina.MaxValue;
    }

    private void OnDisable()
    {
        _stamina.StaminaValueChanged -= OnHealthValueChanged;
        _stamina.MaxStaminaValueChanged -= OnMaxHealthValueChanged;
    }
}
