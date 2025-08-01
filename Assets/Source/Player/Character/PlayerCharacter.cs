using System;
using System.Collections;
using System.Collections.Generic;
using EvolveGames;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class PlayerCharacteristicBase : MonoBehaviour
{
    public abstract int MinValue { get; set; }
    public abstract int MaxValue { get; set; }
    public abstract int CurrentValue { get; set; }
    public abstract void SetValue(int value);
    public abstract void AddValue(int value);
    public abstract void RemoveValue(int value);
    public abstract void Generate();

    public abstract event Action<int> ValueChanged;
}

public abstract class PlayerCharacteristic<T> : PlayerCharacteristicBase where T : MonoBehaviour
{
    [field: SerializeField] public override int MinValue { get; set; }
    [field: SerializeField] public override int MaxValue { get; set; }
    [field: SerializeField] public override int CurrentValue { get; set; }

    
    public override event Action<int> ValueChanged;


    public PlayerCharacter Character { get; private set; }

    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy duplicate instances
            Debug.LogWarning($"Multiple instances of {typeof(T).Name} detected. Destroying duplicate.");
            return;
        }

        Instance = this as T;
        Generate();
    }

    private void Start()
    {
        ValueChanged?.Invoke(CurrentValue);
        OnValueChanged(CurrentValue);
    }

    public override void SetValue(int value)
    {
        CurrentValue = value;
        ValueChanged?.Invoke(CurrentValue);
        OnValueChanged(value);
    }

    public override void AddValue(int value)
    {
        SetValue(CurrentValue + value);
    }

    [Button()]
    public void Add()
    {
        AddValue(1);
    }

    [Button()]
    public void Remove()
    {
        RemoveValue(1);
    }

    public override void RemoveValue(int value)
    {
        SetValue(CurrentValue - value);
    }

    public abstract void OnValueChanged(int value);

    public override void Generate()
    {
        Character = PlayerCharacter.Instance;
        SetValue(Random.Range(MinValue, MaxValue));
        OnValueChanged(CurrentValue);
    }
}

public class PlayerCharacter : MonoBehaviour
{
    [field: SerializeField] public Transform LaserPoint { get; private set; }
    [field: SerializeField] public PlayerController Controller { get; private set; }

    public static PlayerCharacter Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Debug.LogError("There`s one more PlayerCharacter");
    }
}