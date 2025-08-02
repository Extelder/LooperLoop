using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGoalUI : MonoBehaviour
{
    [SerializeField] private GameObject _compleatePanel;

    [SerializeField] private TextMeshProUGUI _goalText;

    [SerializeField] private PlayerGoal _goal;

    private void OnEnable()
    {
        _goal.GoalBootstrapped += OnGoalBootstrapped;
        _goal.GoalCompleated += OnGoalCompleated;
    }

    private void OnGoalCompleated()
    {
        _compleatePanel.SetActive(true);
    }

    private void OnGoalBootstrapped(Goal goal)
    {
        _goalText.text = "Reach " + goal.GoalName + " " + goal.TargetValue;
    }

    private void OnDisable()
    {
        _goal.GoalBootstrapped -= OnGoalBootstrapped;
        _goal.GoalCompleated -= OnGoalCompleated;
    }
}