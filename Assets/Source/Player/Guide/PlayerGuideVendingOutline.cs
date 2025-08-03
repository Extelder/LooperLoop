using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuideVendingOutline : MonoBehaviour
{
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        PlayerGuide.VendingBeginCurrentGoal += OnVendingBeginCurrentGoal;
        PlayerGuide.VendingStopCurrentGoal += OnVendingStopCurrentGoal;
    }

    private void OnVendingStopCurrentGoal()
    {
        _outline.enabled = false;
        PlayerGuide.VendingBeginCurrentGoal -= OnVendingBeginCurrentGoal;
        PlayerGuide.VendingStopCurrentGoal -= OnVendingStopCurrentGoal;
    }

    private void OnVendingBeginCurrentGoal()
    {
        _outline.enabled = true;
    }

    private void OnDisable()
    {
        PlayerGuide.VendingBeginCurrentGoal -= OnVendingBeginCurrentGoal;
        PlayerGuide.VendingStopCurrentGoal -= OnVendingStopCurrentGoal;
    }
}
