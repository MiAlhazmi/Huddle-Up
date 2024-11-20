using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private float _interactDistance = 3f;
    [SerializeField] private LayerMask _layerMask;
    private Interactable _interactable;
    private PlayerUI _playerUI;
    private MyPlayerInputManager _inputManager;

    private bool _interactableFound;
    private bool _didInteract;  // to check if the player pressed the interaction button

    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Interactable");
    }

    void Start()
    {
        _cam = GetComponent<PlayerLook>().GetCam(); // because we already have a camera on PLayerLook script which is attached to the player gameobject
        _playerUI = GetComponent<PlayerUI>();
        _inputManager = GetComponent<MyPlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerUI.UpdateInteractionText(string.Empty);
        _interactableFound = false;
        
        // Create a ray at the center of the camera, shooting outwards.
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _interactDistance);
        // RaycastHit hitInfo; // variable to store ray collision information
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _interactDistance, _layerMask))
        {
            // Debug.DrawRay(ray.origin, ray.direction * _interactDistance);
            _interactable = hitInfo.collider.GetComponent<Interactable>();
            _playerUI.UpdateInteractionText(_interactable.promptMessage);
            _interactableFound = true;
        }
        _didInteract = false;
    }

    public void Interact()
    {
        Debug.Log("Attempt to interact");
        if (!_interactableFound) return;
        if (_interactable != null)
        {
            _interactable.BaseInteract(gameObject);
        }
    }

    public bool Interacted(InputAction.CallbackContext context)
    {
        return context.action.triggered;
    }
    public void SetDidInteract(bool interacted)
    {
        _didInteract = interacted;
    }
}
