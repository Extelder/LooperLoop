using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuideEbakaInvokerOutline : MonoBehaviour
{
    [SerializeField] private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        PlayerGuide.EbakaInvokerBeginCurrentGoal += OnEbakaInvokerBeginCurrentGoal;
        PlayerGuide.EbakaInvokerStopCurrentGoal += OnEbakaInvokerStopCurrentGoal;
    }

    private void OnEbakaInvokerStopCurrentGoal()
    {
        _outline.enabled = false;
        PlayerGuide.EbakaInvokerBeginCurrentGoal -= OnEbakaInvokerBeginCurrentGoal;
        PlayerGuide.EbakaInvokerStopCurrentGoal -= OnEbakaInvokerStopCurrentGoal;
    }

    private void OnEbakaInvokerBeginCurrentGoal()
    {
        _outline.enabled = true;
    }

    private void OnDisable()
    {
        PlayerGuide.EbakaInvokerBeginCurrentGoal -= OnEbakaInvokerBeginCurrentGoal;
        PlayerGuide.EbakaInvokerStopCurrentGoal -= OnEbakaInvokerStopCurrentGoal;
    }
}
