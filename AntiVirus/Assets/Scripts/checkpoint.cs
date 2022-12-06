using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider){
        Debug.Log(collider.tag);
        if (collider.tag == "Guard"){
            collider.SendMessage("nextCheckpoint");
        }
    }
}
