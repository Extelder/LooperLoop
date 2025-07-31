using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private int _startValue = 0;
    [SerializeField] private int _minValue = 0;

    [field: SerializeField] public int CurrentValue { get; private set; }

    private const int maxValue = Int32.MaxValue;

    public static PlayerWallet Instance { get; private set; }

    public event Action<int> ValueChanged;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Debug.LogError("There`s one more PlayerWallet");
    }

    private void Start()
    {
        Add(_minValue);
    }

    public bool TrySpent(int value)
    {
        if (CurrentValue - value < _minValue)
        {
            return false;
        }

        CurrentValue -= value;
        ValueChanged?.Invoke(CurrentValue);
        return true;
    }

    public void Add(int value)
    {
        if (CurrentValue + value > maxValue)
        {
            CurrentValue = maxValue;
            ValueChanged?.Invoke(CurrentValue);
            return;
        }

        CurrentValue += value;
        ValueChanged?.Invoke(CurrentValue);
    }
}