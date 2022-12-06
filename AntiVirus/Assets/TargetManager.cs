using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject circuit;
    [SerializeField] private GameObject player = null;
    [SerializeField] private List<Vector3> checkpoints;
    [SerializeField] private int checkPointIndex;
    [SerializeField] private bool isTrackingPlayer = false;
    void Start()
    {
        // Defining the patrol checkpoints from circuits children
        if(circuit != null){
            foreach(Transform t in circuit.transform){
                checkpoints.Add(t.position);
            }
            gameObject.GetComponent<GuardMovement>().setTarget(checkpoints[checkPointIndex]);
        } else{
            // Telling the guard not to target anything if there is no patrol for it
            gameObject.GetComponent<GuardMovement>().setTargeting(false);
        }
    }

    public Vector3 targetRequest(){ // returns a target for the guard to follow upon request

        Debug.Log("Request for Target Received");
        if (isTrackingPlayer){  // If it reaches a player it has to keep following it.
                                // this code is likely to be rarely called/obsolete eventually
            Debug.Log("Player Still Being Tracked - Return");
            return player.transform.position;
        }

        else if (gameObject.GetComponent<GuardPathing>().GetCount() != 0){ // Returns the next node on the trail back to its starting position 
                                    // after following and then losing a player
            Debug.Log("Guard is returning to pre player tracking location - assigning next node from trail");
            // This is a bit weird but this makes sure that we dont skip a checkpoint in the guards
            // patrol. 
            if (gameObject.GetComponent<GuardPathing>().GetCount() == 1){
                if (checkPointIndex == 0){
                    checkPointIndex = checkpoints.Count - 1;
                } else {
                    checkPointIndex--;
                }
            }
            return gameObject.GetComponent<GuardPathing>().Next(); 
        } 
        
        else if (checkpoints != null){ // Returns the next checkpoint on the guards patrol
            Debug.Log("Guard is On Patrol - Returning next checkpoint on patrol");
            checkPointIndex = (checkPointIndex + 1) % checkpoints.Count;
            return checkpoints[checkPointIndex]; 
        } 
        else { // Tells the guard not to target anything. 
            Debug.Log("Guard should remain where it is");
            gameObject.GetComponent<GuardMovement>().setTargeting(false);
            return new Vector3(0,0,0);
        }
    }

    public void LockOnPlayer(GameObject p){
        Debug.Log("Received message from Player Detector to Lock on Player");
        if (player == null){
            Debug.Log("No player Currently Assigned - Assigning Player detected by Player Detector");
            player = p;
            Debug.Log("Sending Message to Guard Movement to begin tracking player");
            gameObject.GetComponent<GuardMovement>().setPlayer(p);
            isTrackingPlayer = true;
            gameObject.GetComponent<GuardPathing>().StartTracking();
        }
    }
    public void LoseLock(){
        Debug.Log("Message received from Player Detector to Lose Lock on player");
        player = null;
        isTrackingPlayer = false;
        gameObject.GetComponent<GuardPathing>().StopTracking();
        gameObject.GetComponent<GuardMovement>().LosePlayer();
    }
}
