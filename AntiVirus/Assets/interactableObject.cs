using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableObject : MonoBehaviour
{
    public GameObject objectOfInteraction;
    public string functionToCall;
    public List<string> accessTags;
    private bool playerInRange = false;

    void Update(){
        if (playerInRange && Input.GetKeyDown("e")){
            callFunction();
        }
    }

    void OnTriggerEnter(Collider collider){
        if (accessTags.Contains(collider.tag)){
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider collider){
        if (accessTags.Contains(collider.tag)){
            playerInRange = false;
        }
    }

    private void callFunction(){
        objectOfInteraction.SendMessage(functionToCall);
    }

}
