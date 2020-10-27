using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    [SerializeField]
    private GameObject _stage1;

    [SerializeField]
    private GameObject _stage2;

    [SerializeField]
    private GameObject _stage3;

    [SerializeField]
    private bool _onElevator;

    private enum direction
    {
        up,
        down
    }

    private direction _direction;

    private Vector3 _movePosition;

    private float _time;

    public static Action OnElevatorStop;

    private void Start()
    {
        _direction = direction.down;
        _movePosition = this.transform.position;
        ElevatorButton.OnElevatorMoving += OnElevator;
    }

    private void FixedUpdate()
    {

        if (_onElevator)
        {
            if (_movePosition == _stage1.transform.position && _direction == direction.down)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, _stage2.transform.position, 4 * Time.deltaTime);
            }
            else if (_movePosition == _stage2.transform.position && _direction == direction.down)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, _stage3.transform.position, 4 * Time.deltaTime);
            }

            else if (_movePosition == _stage3.transform.position && _direction == direction.up)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, _stage2.transform.position, 4 * Time.deltaTime);
            }
            else if (_movePosition == _stage2.transform.position && _direction == direction.up)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, _stage1.transform.position, 4 * Time.deltaTime);
            }


            else
            {
                Debug.Log("position undefined");
                Debug.Log("this.transform position = " + this.transform.position.x + " " + this.transform.position.y + " " + this.transform.position.z);
                Debug.Log("_stage1.transform position = " + _stage1.transform.position.x + " " + _stage1.transform.position.y + " " + _stage1.transform.position.z);
            }
        }
        


        if (this.transform.position == _stage1.transform.position ||
            this.transform.position == _stage2.transform.position ||
            this.transform.position == _stage3.transform.position)
        {
            StartCoroutine(myRoutine(this.transform.position));
        }


        if (_onElevator == true && this.transform.position == _stage1.transform.position && _direction == direction.up)
        {
            _onElevator = false;

            if (OnElevatorStop != null)
                OnElevatorStop();
        }
       
    }

    

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.transform.tag == "Player")
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.transform.tag == "Player")
            Player.transform.parent = null;
    }

    private IEnumerator myRoutine(Vector3 newRoute)
    {
        yield return new WaitForSeconds(3f);


        if (this.transform.position == _stage1.transform.position)
        {
            _movePosition = newRoute;
            _direction = direction.down;
        }
        else if (this.transform.position == _stage2.transform.position)
        {
            _movePosition = newRoute;
        }
        else if (this.transform.position == _stage3.transform.position)
        {
            _movePosition = newRoute;
            _direction = direction.up;
        }
    }

    private void OnElevator()
    {
        _onElevator = true;
    }


}
