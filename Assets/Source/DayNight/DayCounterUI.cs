using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayText;

    private void Start()
    {
        DayCounter.Instance.DayChanged += OnDayChanged;
    }

    private void OnDayChanged(int current)
    {
        _dayText.text = "D:" + current.ToString();
    }

    private void OnDisable()
    {
        DayCounter.Instance.DayChanged -= OnDayChanged;
    }
}