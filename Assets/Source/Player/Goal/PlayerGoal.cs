using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public abstract class Goal
{
    [field: SerializeField] public string GoalName { get; protected set; }
    [field: SerializeField] public int TargetValue { get; protected set; }

    public Action GoalCompleated;

    public virtual void Activate(Action goalCompleated)
    {
        GoalCompleated = goalCompleated;
    }
}

[Serializable]
public class CharacteristicsGoal<T> : Goal where T : PlayerCharacteristicBase
{
    public override void Activate(Action goalCompleated)
    {
        base.Activate(goalCompleated);
        PlayerCharacteristic<T>.Instance.ValueChanged += OnValueChanged;
        Debug.LogError(GoalName);
    }

    ~CharacteristicsGoal()
    {
        PlayerCharacteristic<T>.Instance.ValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        if (value >= TargetValue)
        {
            GoalCompleated?.Invoke();
            Debug.LogError("POBEDA");
        }
    }
}

public class MoveCharacteristicGoal : CharacteristicsGoal<MoveCharacteristic>
{
}

public class AttackSpeedCharacteristicGoal : CharacteristicsGoal<MeleeSpeedCharacteristic>
{
}

public class MeleeCriticalAttackCharacteristicGoal : CharacteristicsGoal<MeleeCriticalAttackCharacteristics>
{
}

public class JumpCharacteristicGoal : CharacteristicsGoal<JumpCharacteristic>
{
}

public class PlayerStaminaCharacteristicsGoal : CharacteristicsGoal<PlayerStaminaCharacteristics>
{
}


public class PlayerGoal : MonoBehaviour
{
    [SerializeReference] [SerializeReferenceButton]
    private Goal[] _goals;

    public Goal CurrentGoal { get; private set; }

    public event Action GoalCompleated;
    public event Action<Goal> GoalBootstrapped;

    private void OnEnable()
    {
        Time.timeScale = 1;
        GoalCompleated += OnGoalCompleated;
    }

    private void OnGoalCompleated()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        GoalCompleated -= OnGoalCompleated;
    }

    private void Start()
    {
        CurrentGoal = _goals[Random.Range(0, _goals.Length)];
        CurrentGoal.Activate(GoalCompleated);
        GoalBootstrapped?.Invoke(CurrentGoal);
    }
}