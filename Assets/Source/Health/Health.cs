using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [field: SerializeField] public int MaxValue { get; private set; }
    public int CurrentValue { get; private set; }

    public event Action<int> HealthValueChanged;
    public event Action<int> MaxHealthValueChanged;
    public event Action<int> OnHealedToMax;
    public event Action<int> Damaged;
    public event Action<int> Healed;
    public event Action Dead;

    public virtual void TakeDamage(int value)
    {
        if (IsDead())
            return;

        if (CurrentValue - value > 0)
        {
            ChangeHealthValue(CurrentValue - value);
            Damaged?.Invoke(CurrentValue);
            return;
        }

        Damaged?.Invoke(CurrentValue);

        Dead?.Invoke();
        Death();
    }

    public void Heal(int value)
    {
        if (IsDead())
            return;
        if (CurrentValue + value < MaxValue)
        {
            Healed?.Invoke(CurrentValue);
            ChangeHealthValue(CurrentValue + value);
            return;
        }

        HealToMax();
        Healed?.Invoke(CurrentValue);
    }

    public bool IsDead() => CurrentValue <= 0;

    public void SetMaxHealth(int value)
    {
        MaxValue = value;
        MaxHealthValueChanged?.Invoke(MaxValue);
        if (CurrentValue > MaxValue)
        {
            ChangeHealthValue(MaxValue);
        }
    }

    public void HealToMax()
    {
        ChangeHealthValue(MaxValue);
        OnHealedToMax?.Invoke(MaxValue);
    }

    public abstract void Death();

    public virtual void ChangeHealthValue(int value)
    {
        if (CurrentValue >= 0)
        {
            CurrentValue = value;
            HealthValueChanged?.Invoke(CurrentValue);
        }
    }
}