using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkin : MonoBehaviour
{
    [SerializeField] private GameObject[] _skins;

    [SerializeField] private MeleeSettings _settings;

    private GameObject _currentSkin;

    private void OnEnable()
    {
        _settings.ItemChanged += OnItemChanged;
    }

    private void OnItemChanged(MeleeItem item)
    {
        if (_currentSkin == _skins[item.Id])
            return;

        _currentSkin?.SetActive(false);
        _currentSkin = _skins[item.Id];
        _currentSkin.SetActive(true);
    }

    private void OnDisable()
    {
        _settings.ItemChanged -= OnItemChanged;
    }
}