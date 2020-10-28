using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMoving : MonoBehaviour
{
    public static bool _nearLadderSystem;
    private void OnTriggerEnter(Collider other)
    {
        _nearLadderSystem = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _nearLadderSystem = false;
    }

    private void OnTriggerStay(Collider other)
    {
        _nearLadderSystem = true;
    }

}
