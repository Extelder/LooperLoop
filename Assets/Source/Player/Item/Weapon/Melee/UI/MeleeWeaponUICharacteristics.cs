using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeWeaponUICharacteristics : MonoBehaviour
{
    [SerializeField] private MeleeSettings _meleeSettings;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _speedText;

    private void OnEnable()
    {
        _meleeSettings.ItemChanged += OnItemChanged;
    }

    private void OnItemChanged(MeleeItem currentItem)
    {
        _damageText.text = "Damage: " + currentItem.Damage;
        _speedText.text = "Attack Speed: " + currentItem.Speed;
    }

    private void OnDisable()
    {
        _meleeSettings.ItemChanged -= OnItemChanged;
    }
}
