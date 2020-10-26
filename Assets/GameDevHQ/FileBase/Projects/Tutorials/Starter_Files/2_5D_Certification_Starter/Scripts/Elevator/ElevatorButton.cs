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


    private void Start()
    {
        this.GetComponent<MeshRenderer>().material = _buttonOff;
    }

    private void OnTriggerStay(Collider Player)
    {
      if(Player.transform.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (_elevatorOn == false)
                {
                    this.GetComponent<MeshRenderer>().material = _buttonOn;
                    _elevatorOn = true;
                }
                else if (_elevatorOn == true)
                {
                    this.GetComponent<MeshRenderer>().material = _buttonOff;
                    _elevatorOn = false;
                    
                }
                    
            }
        }
    }
}
