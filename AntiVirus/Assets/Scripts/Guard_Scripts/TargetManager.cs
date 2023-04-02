using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    // Scripts
    private GuardMovement movementControl; // Controls the physical movement of the guard
    private GuardPathing pathControl; // Tracks the path of the guard

    // Circuit is the guards patrol
    [SerializeField] private Stack<Vector3> path; // Path back to prior position
    [SerializeField] private GameObject circuit; // Checkpoints on patrol
    [SerializeField] private List<Vector3> checkpoints; // Checkpoints
    [SerializeField] private int index; // Current checkpoint index

    


    private void Awake(){
        movementControl = gameObject.GetComponent<GuardMovement>();
        pathControl = gameObject.GetComponent<GuardPathing>();
        path = new Stack<Vector3>();
    }

    void Start()
    {
        // Defining the patrol checkpoints from circuits children
        if(circuit != null){
            foreach(Transform t in circuit.transform){
                checkpoints.Add(t.position);
            }
            movementControl.setTarget(checkpoints[index]);
        } else{
            // Telling the guard not to target anything if there is no patrol for it
            movementControl.setTargeting(false);
        }
    }

    public Vector3 targetRequest(){ // returns a target for the guard to follow upon request
        if (pathControl.GetCount() > 0){
            return pathControl.Next();
        } else if (checkpoints.Count > 0){
            index = (index + 1) % checkpoints.Count;;
            return checkpoints[index];
        } else {
            movementControl.setTargeting(false);
            return new Vector3(0,0,0); // Placeholder position, the guard shouldnt actually move at this point. 
        }
    }

    public void LockOnPlayer(GameObject p){
        Debug.Log("Received message from Player Detector to Lock on Player");
        pathControl.Tracking(true);
        movementControl.setPlayer(p);
        if(index > 0){index++;} else {index = checkpoints.Count - 1;}
    }
    public void LoseLock(){
        Debug.Log("Message received from Player Detector to Lose Lock on player");
        pathControl.Tracking(false);
        movementControl.LosePlayer();
    }
}
