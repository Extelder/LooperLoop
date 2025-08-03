using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VendingMachineBuyable : MonoBehaviour ,IBuyable, IInteractable
{
    [field: SerializeField] public BuyableItem Item { get; set; }
    [field: SerializeField] public Outline Outline { get; set; }

    public static event Action VendingDetected;
    public static event Action VendingBuffBought;
    
    public abstract void OnBought();
    public bool TryBuy()
    {
        if (PlayerWallet.Instance.TrySpent(Item.Price))
        {
            VendingBuffBought?.Invoke();
            OnBought();
            return true;
        }
        return false;
    }

    public void Interact()
    {
        TryBuy();
    }

    public void InteractDetected()
    {
        VendingDetected?.Invoke();
        Outline.enabled = true;
    }

    public void InteractLost()
    {
        Outline.enabled = false;
    }
}
