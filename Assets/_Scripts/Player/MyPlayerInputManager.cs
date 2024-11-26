using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyPlayerInputManager : MonoBehaviour
{
    public MyPlayerInputManager instance;
    
    public static PlayerInput playerInput;      // made it static, so I can switch control to UI control to all players at once
    public PlayerInputActionsDefault.PlayerActions PlayerActions;
    
    private PlayerInputActionsDefault _playerInputActions;
    private PlayerMovement movement;
    private PlayerLook look;
    private PlayerHit hit;
    private PlayerInteract playerInteract;

    [SerializeField] private Vector2 inputVector = Vector2.zero;
    [SerializeField] private Vector2 inputLook = Vector2.zero;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (!TryGetComponent<PlayerInput>(out playerInput))
        {
            Debug.LogError("PlayerInput Component is missing!");
        }
        _playerInputActions = new PlayerInputActionsDefault();
        PlayerActions = _playerInputActions.Player;
        
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        hit = GetComponent<PlayerHit>();
        playerInteract = GetComponent<PlayerInteract>();
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
        if (context.performed) 
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

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) {
            if (!PauseGameController.Instance.HandlePauseRequest(gameObject))
                Debug.Log("Couldn't Pause");
        }
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
        PlayerActions.Enable();
    }
    
    private void OnDisable()
    {
        PlayerActions.Disable();
    }
}
