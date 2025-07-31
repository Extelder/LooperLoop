using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSSettings : SliderSettings
{
    [SerializeField] private TextMeshProUGUI _currentValueText;

    private void Start()
    {
        float maxFpsSaved = PlayerPrefs.GetInt("FPS", 60);

        Slider.value = maxFpsSaved;
    }

    protected override void OnSliderValueChanged(float value)
    {
        Application.targetFrameRate = (int) value;
        PlayerPrefs.SetInt("FPS", (int) value);
        _currentValueText.text = value.ToString();
    }
}