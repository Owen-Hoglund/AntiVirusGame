using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableObject : MonoBehaviour
{
    public GameObject objectOfInteraction;
    public string functionToCall;
    public List<string> accessTags;


    void OnTriggerEnter(Collider collider){
        if (accessTags.Contains(collider.tag)){
            objectOfInteraction.SendMessage(functionToCall);
        }
    }

}
