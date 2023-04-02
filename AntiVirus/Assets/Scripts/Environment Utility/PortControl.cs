using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortControl : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject leftRoom;
    public GameObject rightRoom;

    private bool playerInRange;
    private bool underway;

    void Start(){
        playerInRange = false;
    }
    void Update(){
        if (playerInRange && !underway && Input.GetKeyDown("e")){
            underway = true;
            begin();
        }
    }

    private void begin(){
        if (leftDoor.GetComponent<DoorControl>().getOpen()){
            leftDoor.SendMessage("interact");
        } else{
            rightDoor.SendMessage("interact");
        }
    }

    private void next(){
        if(playerInRange && underway){        
            if (leftRoom.activeSelf){
                leftRoom.SetActive(false);
                rightRoom.SetActive(true);
                rightDoor.SendMessage("interact");
            } else {
                leftRoom.SetActive(true);
                rightRoom.SetActive(false);
                leftDoor.SendMessage("interact");
            }
        }
    }

    private void done(){
        if (underway){
            underway = false;
        }
    }

    void OnTriggerEnter(Collider collider){
        if (collider.tag == "Player"){
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider collider){
        if (collider.tag == "Player"){
            playerInRange = false;
        }
    }
}
