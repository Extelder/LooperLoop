using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private RaycastSettings _raycast;

    private RaycastHit _hit;

    private IInteractable _currentInteractable;

    public event Action InteractedLost;
    public event Action InteractedDetected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TryToInteract();

        if (Physics.Raycast(_raycast.origin.position, _raycast.origin.forward, out _hit, _raycast.Range,
            _raycast.LayerMask))
        {
            if (_hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (_currentInteractable != null)
                    _currentInteractable.InteractLost();

                _currentInteractable = interactable;
                _currentInteractable.InteractDetected();
                InteractedDetected?.Invoke();
                return;
            }

            if (_currentInteractable != null)
                _currentInteractable.InteractLost();
            InteractedLost?.Invoke();
            _currentInteractable = null;
        }


        if (_currentInteractable != null)
            _currentInteractable.InteractLost();
        InteractedLost?.Invoke();
        _currentInteractable = null;
    }

    public void TryToInteract()
    {
        if (_currentInteractable == null)
            return;
        _currentInteractable.Interact();
    }
}