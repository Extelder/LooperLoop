using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SliderSettings : MonoBehaviour
{
    [field: SerializeField] protected Slider Slider { get; private set; }

    private void OnEnable()
    {
        Slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    protected abstract void OnSliderValueChanged(float value);

    private void OnDisable()
    {
        Slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}