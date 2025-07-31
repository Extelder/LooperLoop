using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeItemInteractable : MonoBehaviour, IInteractable
{
    [field: SerializeField] public MeleeItem Item;

    public event Action Detected;
    public event Action Lost;


    public void Interact()
    {
    }

    public void InteractDetected()
    {
        Detected?.Invoke();
    }

    public void InteractLost()
    {
        Lost?.Invoke();
    }
}