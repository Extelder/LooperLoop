using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VolumeSettings : SliderSettings
{
    [SerializeField] private TextMeshProUGUI _currentValueText;

    protected override void OnEnableVirtual()
    {
        float volume = PlayerPrefs.GetInt("Volume", 100);
        base.OnEnableVirtual();
        Slider.value = volume;
    }

    protected override void OnSliderValueChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetInt("Volume", (int) value);
        _currentValueText.text = value.ToString();
    }
}