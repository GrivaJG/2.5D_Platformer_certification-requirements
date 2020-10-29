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

        if (Input.GetAxisRaw("Horizontal") != 0 && _animator.GetBool("isLadderClimbing") == false)
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


            
        }

        if (Input.GetButtonDown("Jump") && _isGround && _animator.GetBool("isGrabLedge") == false && _animator.GetBool("isRoll") == false)
        {
            _velocity.y += _jump;
            _animator.SetBool("isJumping", true);
           
        }
        else if (Input.GetButtonDown("Jump") && _isGround == false && _doubleJump && _animator.GetBool("isGrabLedge") == false && _animator.GetBool("isRoll") == false)
        {
            
            
            _velocity.y += Mathf.Sqrt(_jump * -0.2f * _gravity);
            _doubleJump = false;
            _animator.SetBool("isJumping", true);
            
            _animator.SetBool("isLadderClimbing", false);
            _animator.SetFloat("isRunningUp", 0f);

        }
        else if (Input.GetButtonDown("Jump") && _isGround == false && _animator.GetBool("isGrabLedge") == true && _animator.GetBool("isRoll") == false)
        {
            _animator.SetTrigger("isClimbingUp");
            
        }

       
        if (LadderMoving._nearLadderSystem)
        {
            _animator.SetFloat("isRunningUp", Input.GetAxisRaw("Vertical"));
            var ModelRotation = this.transform.Find("Model");

            if (Input.GetAxisRaw("Vertical") == 1)
            {   
                _animator.SetBool("isLadderClimbing", true);
                ModelRotation.transform.localEulerAngles = new Vector3(0, -90, 0);
                _direction = new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                _controller.Move(_direction * 2 * Time.deltaTime);
                _animator.SetBool("isJumping", false);


            }
            else if (Input.GetAxisRaw("Vertical") == -1 && _isGround == false)
            {
                _animator.SetBool("isLadderClimbing", true);
                ModelRotation.transform.localEulerAngles = new Vector3(0, -90, 0);
                _direction = new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                _controller.Move(_direction * 2 * Time.deltaTime);
                _animator.SetBool("isJumping", false);
            }
            else if (Input.GetAxisRaw("Vertical") == -1 && _isGround == true)
            {
                _animator.SetBool("isLadderClimbing", false);
            }

            

            if (_animator.GetBool("isLadderClimbing") == true)
            {
                _velocity.y = 0;
               
                _velocity.y -= _gravity * Time.deltaTime;
            }
                
        }
        else
        {
            _animator.SetBool("isLadderClimbing", false);
            _animator.SetFloat("isRunningUp", 0f);
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && _isGround)
        {
            if (_animator.GetFloat("isRunning") != 0)
            {
                _animator.SetBool("isRoll", true);
                StartCoroutine(myRoll());
            }
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
            transform.position = new Vector3(-4.84f, this.transform.position.y + 7.48f, this.transform.position.z - 1.6f);
        _controller.enabled = true;
        _animator.SetBool("isGrabLedge", false);
       
    }

    private IEnumerator myRoll()
    {
        yield return new WaitForSeconds(1.1f);
        _animator.SetBool("isRoll", false);
    }

}
