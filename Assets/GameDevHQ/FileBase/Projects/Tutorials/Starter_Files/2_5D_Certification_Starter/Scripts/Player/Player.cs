using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    [SerializeField] private float _gravity = -9.81f;

    [SerializeField] private float _jump = 20f;

    private bool _isGround;

    private CharacterController _controller;

    private Vector3 _velocity;

    private bool _doubleJump;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGround = _controller.isGrounded;

        Debug.Log(_isGround);
        
        if (_isGround && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        Vector3 move = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        _controller.Move(move * _speed * Time.deltaTime);

        

        if (Input.GetButtonDown("Jump") && _isGround)
        {            
            _velocity.y += _jump;
        }
        else if (Input.GetButtonDown("Jump") && _isGround == false && _doubleJump)
        {
            Debug.Log("double jump");
            _velocity.y += Mathf.Sqrt(_jump * -0.2f * _gravity);
            _doubleJump = false;
        }

        if (_isGround)
            _doubleJump = true;

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

}
