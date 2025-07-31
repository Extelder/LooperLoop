using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeItemInteractableInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _attackSpeedText;
    [SerializeField] private TextMeshPro _damageText;

    [SerializeField] private GameObject _infoPanel;

    [SerializeField] private MeleeItemInteractable _itemInteractable;

    private void OnEnable()
    {
        _itemInteractable.Detected += OnDetected;
        _itemInteractable.Lost += OnLost;
    }

    private void OnLost()
    {
        _infoPanel.SetActive(false);
    }

    private void OnDetected()
    {
        _nameText.text = _itemInteractable.Item.name;
        _attackSpeedText.text = _itemInteractable.Item.Speed.ToString();
        _damageText.text = _itemInteractable.Item.Damage.ToString();

        _infoPanel.SetActive(true);
    }

    private void OnDisable()
    {
        _itemInteractable.Detected -= OnDetected;
        _itemInteractable.Lost -= OnLost;
    }
}