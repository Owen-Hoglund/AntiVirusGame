using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    // Choose starting position and speed
    public bool startsOpen;
    public float speed;

    // Private variables
    private bool isOpen;
    private bool underway;
    private Vector3 openPosition;
    private Vector3 closedPosition;
    void Start(){
        // Initializes values based on whether the door is starting open or closed
        if (startsOpen){
            isOpen = true;
            openPosition = transform.position;
            closedPosition = transform.position + new Vector3(0, transform.localScale.y);
        } else {
            isOpen = false;
            openPosition = transform.position - new Vector3(0, transform.localScale.y);
            closedPosition = transform.position;
        }
        underway = false;
    }
    void Update()
    {
        if (underway){
            move();
        }
    }

    public void openOrClose(){
        underway = !underway;
    }

    private void move(){
        if (isOpen){
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, speed/100);
            if (transform.position == closedPosition){
                Debug.Log("Door Closed");
                isOpen = !isOpen;
                underway = !underway;
            }
        } else{
            transform.position = Vector3.MoveTowards(transform.position, openPosition, speed/100);
            if (transform.position == openPosition){
                Debug.Log("Door Open");
                isOpen = !isOpen;
                underway = !underway;
            }
        }
    }
}
