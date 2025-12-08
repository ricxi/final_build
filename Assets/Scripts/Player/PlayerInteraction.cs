using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _isInteracting;
    private bool _canInteract;
    private IInteractable _interactable;

    private void Awake()
    {
        _canInteract = false;
        _interactable = null;
    }

    private void Update()
    {
        GetInput();
        Interact();
    }

    private void GetInput()
    {
        _isInteracting = Input.GetKey(KeyCode.X);
    }

    private void Interact()
    {
        if (_canInteract && _isInteracting)
        {
            _interactable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _interactable = collision.gameObject.GetComponent<IInteractable>();
        _canInteract = _interactable != null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactable = null;
        _canInteract = false;
    }
}
