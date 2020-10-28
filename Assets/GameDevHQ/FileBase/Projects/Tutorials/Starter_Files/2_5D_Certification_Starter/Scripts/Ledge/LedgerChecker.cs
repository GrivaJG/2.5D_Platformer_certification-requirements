using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgerChecker : MonoBehaviour
{
    [SerializeField]
    private Vector3 _handPose;




    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ledge")
        {
            Debug.Log("in Ledge_Checker");
            var player = other.transform.parent.GetComponent<Player>();
            _handPose = other.transform.parent.GetComponent<Transform>().position;
            var modelRotation = other.transform.parent.transform.Find("Model");
            if (modelRotation == null)
                Debug.Log("modelRotation is NULL");

            if (modelRotation.transform.localEulerAngles.y == 0)
                _handPose.z += 0.6f;
            else
                _handPose.z -= 0.6f;
            other.transform.parent.GetComponent<Transform>().position = _handPose;
            Debug.Log("_handPose: " + _handPose.x + " " + _handPose.y + " " + _handPose.z);

            if (player != null)
                player.GrabLedge(_handPose);
        }
    }
}
