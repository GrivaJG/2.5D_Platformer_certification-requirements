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

            if (player != null)
                player.GrabLedge(_handPose);
        }
    }
}
