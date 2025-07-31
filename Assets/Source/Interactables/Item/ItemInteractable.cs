using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour, IInteractable
{
    [field: SerializeField] public Item Item;
    
    public void Interact()
    {
        
    }

    public void InteractDetected()
    {
    }

    public void InteractLost()
    {
    }
}
