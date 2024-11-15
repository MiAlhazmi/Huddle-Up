using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TagInputManager : GameModeInputManager
{
    private PlayerInputActionsDefault _playerInputActions;
    private PlayerInputActionsDefault.PlayerActions _playerActions;
    private PlayerInput playerInput;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerLook look;
    [SerializeField] private PlayerHit hit;
    private PlayerInteract playerInteract;

    [SerializeField] private Vector2 inputVector = Vector2.zero;
    [SerializeField] private Vector2 inputLook = Vector2.zero;
    
    void Awake()
    {
        Debug.Log("Awake is called in TagInputManager");
        _playerInputActions = new PlayerInputActionsDefault();
        _playerActions = _playerInputActions.Player;
        playerInput = GetComponent<PlayerInput>();
        
        movement = GetComponentInChildren<PlayerMovement>(true);
        look = GetComponentInChildren<PlayerLook>(true);
        hit = GetComponentInChildren<PlayerHit>(true);
        playerInteract = GetComponentInChildren<PlayerInteract>(true);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        // pass movement input to PlayerMovement component
        inputVector = context.ReadValue<Vector2>();
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        // pass look(camera) input to PlayerLook component
        // look.ProcessLook() context.action.triggered;
        inputLook = context.ReadValue<Vector2>();
    }
    
    public void OnHit(InputAction.CallbackContext context)
    {
        // I put performed to avoid triple calls to the Hit() function (started, performed, cancelled)
        if (context.performed) 
            hit.Hit();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) 
            movement.Jump();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)  
            movement.Crouch();
    }
    
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            movement.Sprint();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            playerInteract.Interact();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.ProcessMove(inputVector);
    }

    private void LateUpdate()
    {
        look.ProcessLook(inputLook);
    }
    
    

    private void OnEnable()
    {
        _playerActions.Enable();
        playerInput.SwitchCurrentActionMap("Player");
        // _playerActions.Jump.started += OnJump;
        // _playerActions.Move.performed += OnMove;
    }
    
    private void OnDisable()
    {
        _playerActions.Disable();
    }
}
