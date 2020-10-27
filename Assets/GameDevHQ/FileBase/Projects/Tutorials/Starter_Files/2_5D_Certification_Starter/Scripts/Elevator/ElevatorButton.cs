using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{

    [SerializeField]
    private Material _buttonOn;

    [SerializeField]
    private Material _buttonOff;

    private bool _elevatorOn;

    private bool _nearTheButton;

    public static Action OnElevatorMoving;


    private void Start()
    {
        this.GetComponent<MeshRenderer>().material = _buttonOff;

        ElevatorMove.OnElevatorStop += OffElevator;
        ElevatorButton.OnElevatorMoving += OnElevator;
    }

    private void OnTriggerStay(Collider Player)
    {
        if (Player.transform.tag == "Player")
        {
            _nearTheButton = true;
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        _nearTheButton = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_elevatorOn == false && _nearTheButton == true)
            {
                this.GetComponent<MeshRenderer>().material = _buttonOn;
                _elevatorOn = true;

                if (OnElevatorMoving != null)
                    OnElevatorMoving();
            }
            
        }        
    }

    private void OffElevator()
    {
        _elevatorOn = false;
        this.GetComponent<MeshRenderer>().material = _buttonOff;
    }

    private void OnElevator()
    {
        _elevatorOn = true;
    }

    private void OnDestroy()
    {
        ElevatorMove.OnElevatorStop -= OffElevator;
        ElevatorButton.OnElevatorMoving -= OnElevator;
    }
}
