using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSettings : MonoBehaviour
{
    [SerializeField] private MeleeItem _startItem;

    public MeleeItem CurrentItem { get; private set; }

    public event Action<MeleeItem> ItemChanged;

    private void OnEnable()
    {
        MeleeItemInteractable.ItemPickued += OnItemPickuped;
    }

    private void OnItemPickuped(MeleeItem item)
    {
        ChangeItem(item);
    }

    private void OnDisable()
    {
        MeleeItemInteractable.ItemPickued += OnItemPickuped;
    }

    private void Start()
    {
        ChangeItem(_startItem);
    }

    public void ChangeItem(MeleeItem item)
    {
        if (CurrentItem == item)
            return;

        CurrentItem = item;
        ItemChanged?.Invoke(CurrentItem);
    }
}