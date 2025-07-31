using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeItemInteractableOutline : MonoBehaviour
{
    [SerializeField] private Outline _outline;

    [SerializeField] private MeleeItemInteractable _itemInteractable;

    private void OnEnable()
    {
        _itemInteractable.Detected += OnDetected;
        _itemInteractable.Lost += OnLost;
    }

    private void OnLost()
    {
        _outline.enabled = false;
    }

    private void OnDetected()
    {
        _outline.enabled = true;
    }

    private void OnDisable()
    {
        _itemInteractable.Detected -= OnDetected;
        _itemInteractable.Lost -= OnLost;
    }
}