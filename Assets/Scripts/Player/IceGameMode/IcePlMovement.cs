using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class IcePlMovement : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _playerVelocity = Vector3.zero;
    private bool _isGrounded;
    private bool _isMoving;
    
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float rotateSpeed = 25f; //Speed the player rotate
    private const float Gravity = -9.8f;
    [SerializeField] private float _jumpHeight = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _controller.isGrounded;
    }
    
    public void ProcessMove(Vector2 input)
    {
        ProcessDirection(input);
        Vector3 moveDir = new Vector3(input.x, 0, input.y);
        _isMoving = moveDir != Vector3.zero; // for animation: to check if the character is walking
        _controller.Move(moveDir * Time.deltaTime * _speed);
        _playerVelocity.y += Gravity * Time.deltaTime;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    public void ProcessDirection(Vector2 input)
    {
        if (input.x != 0 || input.y != 0)
        {
            Vector3 targetDir = new Vector3(input.x, 0, input.y); //Direction of the character
            
            if (targetDir == Vector3.zero)
                targetDir = transform.forward;
            Quaternion tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); //Rotate the character little by little
            transform.rotation = targetRotation;
        }
    }
    
    public void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3.0f * Gravity);
        }
    }
}
