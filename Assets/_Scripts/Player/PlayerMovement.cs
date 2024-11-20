using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    // private Animator _animator;
    private bool _isWalking;
    // private bool isSprinting;        no need for this because I'm keeping track of it anyways (_sprinting)
    // private bool isCrouching;        no need for this because I'm keeping track of it anyways (_crouching)
    // private bool isJumping;
    
    private Vector3 _playerVelocity = Vector3.zero;
    private bool _isGrounded;
    private bool _lerpCrouch = false, _crouching = false, _sprinting = false;
    private float _crouchTimer = 0f; // time takes to crouch and to set up from crouching
    
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _walkSpeed = 5;
    [SerializeField] private float _sprintSpeed = 8 ; 
    [SerializeField] private float _crouchSpeed = 3;
    private const float Gravity = -9.8f;
    [SerializeField] private float _jumpHeight = 1.5f;
    private float playerHeight;
    private float playerCrouchHeight;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        // _animator = GetComponent<Animator>();
        playerHeight = _controller.height;
        playerCrouchHeight = playerHeight / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _controller.isGrounded;
        if (_lerpCrouch)
        {
            _crouchTimer += Time.deltaTime;
            float p = _crouchTimer / 1;
            p *= p;
            if (_crouching)
            {
                _controller.height = Mathf.Lerp(_controller.height, 1, p);
            }
            else
            {
                _controller.height = Mathf.Lerp(_controller.height, 2, p);
            }

            if (p > 1)
            {
                _lerpCrouch = false;
                _crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDir = new Vector3(input.x, 0, input.y);
        _isWalking = moveDir != Vector3.zero; // for animation: to check if the character is walkingwaw
        _controller.Move(transform.TransformDirection(moveDir) * Time.deltaTime * _speed);
        _playerVelocity.y += Gravity * Time.deltaTime;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (_crouching)
        {
            Crouch();
            return;
        }

        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3.0f * Gravity);
        }
    }

    public void Sprint()
    {
        if(_crouching) return;
        
        _sprinting = !_sprinting;
        if (_sprinting)
            _speed = _sprintSpeed;
        else
            _speed = _walkSpeed;
    }

    public void RefreshSprint()
    {
        if (_sprinting) 
            _speed = _sprintSpeed;
        else
            _speed = _walkSpeed;
    }
    
    public void Crouch()
    {
        // _crouching = true
        // // flase
        // false = true
        if (_crouching = !_crouching)   // to flip the value of _crouching and then check if crouching 
            _speed = _crouchSpeed;
        else 
            _speed = _walkSpeed;
        // bara
        _crouchTimer = 0;
        _lerpCrouch = true;
    }
    
    public bool GetIsWalking()
    {
        return _isWalking;
    }
    public bool GetIsSprinting()
    {
        return _sprinting;
    }

    public bool GetIsCrouching()
    {
        return _crouching;
    }

    public void SetSprintSpeed(float paramSpeed)
    {
        _sprintSpeed = paramSpeed;
    }
}
