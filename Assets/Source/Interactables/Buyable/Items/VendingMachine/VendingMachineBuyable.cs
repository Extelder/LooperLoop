using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VendingMachineBuyable : MonoBehaviour ,IBuyable, IInteractable
{
    [field: SerializeField] public BuyableItem Item { get; set; }
    [field: SerializeField] public Outline Outline { get; set; }

    public abstract void OnBought();
    public bool TryBuy()
    {
        if (PlayerWallet.Instance.TrySpent(Item.Price))
        {
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
        Outline.enabled = true;
    }

    public void InteractLost()
    {
        Outline.enabled = false;
    }
}
