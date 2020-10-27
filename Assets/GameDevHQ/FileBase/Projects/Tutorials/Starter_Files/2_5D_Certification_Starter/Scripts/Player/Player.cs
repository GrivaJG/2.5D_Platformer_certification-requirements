using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    [SerializeField] private float _gravity = -9.81f;

    [SerializeField] private float _jump = 20f;

    [SerializeField] private Animator _animator;
    
    private bool _isGround;

    private CharacterController _controller;

    private Vector3 _velocity;
    private Vector3 _direction;

    private bool _doubleJump;
    private bool _jumping;

    

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        CalculateMovement();
       
    }

    private void CalculateMovement()
    {
        _isGround = _controller.isGrounded;

        if (_isGround && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        _direction = new Vector3(0, 0, Input.GetAxisRaw("Horizontal"));
        _controller.Move(_direction * _speed * Time.deltaTime);
        _animator.SetFloat("isRunning", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (_animator.GetBool("isGrabLedge") == false)
            {
                var ModelRotation = this.transform.Find("Model");
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                ModelRotation.localEulerAngles = facing;
            }
          
        }

        if (_isGround)
        {
            _doubleJump = true;
            _animator.SetBool("isJumping", false);


            Debug.Log(_animator.GetBool("isJumping"));
        }

        if (Input.GetButtonDown("Jump") && _isGround && _animator.GetBool("isGrabLedge") == false)
        {
            _velocity.y += _jump;
            _animator.SetBool("isJumping", true);
            Debug.Log(_animator.GetBool("isJumping"));
        }
        else if (Input.GetButtonDown("Jump") && _isGround == false && _doubleJump && _animator.GetBool("isGrabLedge") == false)
        {
            Debug.Log("double jump");
            _velocity.y += Mathf.Sqrt(_jump * -0.2f * _gravity);
            _doubleJump = false;
            _animator.SetBool("isJumping", true);
            Debug.Log(_animator.GetBool("isJumping"));
        }
        else if (Input.GetButtonDown("Jump") && _isGround == false && _animator.GetBool("isGrabLedge") == true)
        {
            _animator.SetTrigger("isClimbingUp");
            
        }


        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void GrabLedge(Vector3 handPose)
    {
        _controller.enabled = false;
        _animator.SetBool("isGrabLedge", true);
        transform.position = handPose;
        _animator.SetFloat("isRunning", 0);
        _animator.SetBool("isJumping", false);
    }

    public void OnClimbingUp()
    {
        var ModelRotation = this.transform.Find("Model");
        if (ModelRotation.transform.localEulerAngles.y == 0)
            transform.position = new Vector3(-4.84f, this.transform.position.y + 7.48f, this.transform.position.z + 1.4f);
        else if (ModelRotation.transform.localEulerAngles.y == 180)
            transform.position = new Vector3(-4.84f, this.transform.position.y + 7.48f, this.transform.position.z - 1.4f);
        _controller.enabled = true;
        _animator.SetBool("isGrabLedge", false);
       
    }

}
