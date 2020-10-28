using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA;

    [SerializeField]
    private Transform _pointB;

    [SerializeField]
    private float _speed = 4;

    private enum direction
    {
        up,
        down
    }

    private direction _direct;

    private Vector3 _move;

    private void Start()
    {
        _direct = direction.up;       
    }


    private void FixedUpdate()
    {
        if (this.transform.position == _pointA.position) // && _direct == direction.up)
        {
            _move = _pointB.position;            
        }
        else if (this.transform.position == _pointB.position) // && _direct == direction.down)
        {
            _move = _pointA.position;
        }

        this.transform.position = Vector3.MoveTowards(this.transform.position, _move, Time.deltaTime * _speed);

    }
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.transform.tag == "Player")
        {
            Player.transform.parent = this.transform;
        }
        
    }

    private void OnTriggerExit(Collider Player)
    {
        if(Player.transform.tag == "Player")
        {
            Player.transform.parent = null;
        }
    }
}
