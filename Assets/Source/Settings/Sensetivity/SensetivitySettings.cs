using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SensetivitySettings : SliderSettings
{
    [SerializeField] private TextMeshProUGUI _currentValueText;

    private PlayerCharacter _character;

    protected override void OnEnableVirtual()
    {
        _character = PlayerCharacter.Instance;
        float sensetivity = PlayerPrefs.GetInt("Sensetivity", 1);
        base.OnEnableVirtual();
        Slider.value = sensetivity;
    }

    protected override void OnSliderValueChanged(float value)
    {
        _character.Controller.lookSpeed = value;
        PlayerPrefs.SetInt("Sensetivity", (int) value);
        _currentValueText.text = value.ToString();
    }
}