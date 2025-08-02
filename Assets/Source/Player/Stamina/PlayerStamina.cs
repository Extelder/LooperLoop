using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [field: SerializeField] public int MaxValue { get; private set; }
    public int CurrentValue { get; private set; }
    
    public event Action<int> StaminaValueChanged;
    public event Action<int> MaxStaminaValueChanged;
    public event Action<int> OnStaminaRecoveredToMax;
    public event Action<int> Wasted;
    public event Action<int> StaminaRecovered;
    public event Action StaminaWasted;

    public void Waste(int value)
    {
        if (CurrentValue - value >= 0)
        {
            ChangeStaminaValue(CurrentValue - value);
            Wasted?.Invoke(CurrentValue);
            return;
        }

        Wasted?.Invoke(CurrentValue);

        StaminaWasted?.Invoke();
    }

    public void Recover(int value)
    {
        if (CurrentValue + value < MaxValue)
        {
            StaminaRecovered?.Invoke(CurrentValue);
            ChangeStaminaValue(CurrentValue + value);
            return;
        }

        RecoverToMax();
        StaminaRecovered?.Invoke(CurrentValue);
    }

    public void SetMaxStamina(int value)
    {
        MaxValue = value;
        MaxStaminaValueChanged?.Invoke(MaxValue);
        if (CurrentValue > MaxValue)
        {
            ChangeStaminaValue(MaxValue);
        }
    }

    public void RecoverToMax()
    {
        ChangeStaminaValue(MaxValue);
        OnStaminaRecoveredToMax?.Invoke(MaxValue);
    }

    public void ChangeStaminaValue(int value)
    {
        if (CurrentValue >= 0)
        {
            CurrentValue = value;
            StaminaValueChanged?.Invoke(CurrentValue);
        }
    }
}
