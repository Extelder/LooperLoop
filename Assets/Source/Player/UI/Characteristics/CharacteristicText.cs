using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacteristicText : MonoBehaviour
{
    [SerializeField] private PlayerCharacteristicBase _characteristic;

    [SerializeField] private TextMeshProUGUI _text;
    
    private void OnEnable()
    {
        _characteristic.ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _text.text = value.ToString();
    }

    private void OnDisable()
    {
        _characteristic.ValueChanged -= OnValueChanged;
    }
}