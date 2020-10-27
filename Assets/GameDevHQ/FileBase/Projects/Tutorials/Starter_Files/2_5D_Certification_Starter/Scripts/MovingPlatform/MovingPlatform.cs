using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
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
