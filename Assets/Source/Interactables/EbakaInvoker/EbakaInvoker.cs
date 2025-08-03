using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbakaInvoker : MonoBehaviour, IInteractable
{
    [SerializeField] private Outline _outline;
    [SerializeField] private GameObject _ebakaPrefab;

    public static event Action EbakaSpawned;
    
    public void Interact()
    {
        EbakaSpawned?.Invoke();
        Instantiate(_ebakaPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void InteractDetected()
    {
        _outline.enabled = true;
    }

    public void InteractLost()
    {
        _outline.enabled = false;
    }
}