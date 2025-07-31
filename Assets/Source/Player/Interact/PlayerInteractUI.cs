using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private PlayerInteract _interact;
    [SerializeField] private GameObject _indicator;

    private void OnEnable()
    {
        _interact.InteractedDetected += OnInteractedDetected;
        _interact.InteractedLost += OnInteractedLost;
    }

    private void OnInteractedLost()
    {
        _indicator.SetActive(false);
    }

    private void OnInteractedDetected()
    {
        _indicator.SetActive(true);
    }

    private void OnDisable()
    {
        _interact.InteractedDetected -= OnInteractedDetected;
        _interact.InteractedLost -= OnInteractedLost;
    }
}