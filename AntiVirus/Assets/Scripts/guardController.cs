using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardController : MonoBehaviour
{
    // All variables used for movement
    private Rigidbody self; // easier than using GetComponent each time we want to access it
    [SerializeField] private float acceleration; // How quickly the guard accelerates toward a target
    [SerializeField] private float hoverHeight; // How high above the ground the guard should hover
    [SerializeField] private float maxVelocity; // Max velocity 

    [SerializeField] private bool lockedOnPlayer; // Whether the guard is locked onto and following a character
    [SerializeField] private GameObject player; // If the guard is locked onto a player, it keeps a reference to it here
    private Vector3 direction; // Realtime calculated direction toward the  current target
    private Vector3 destination; // Location of target ONLY if guard is not tracking a player/on a checkpoint circuit
    [SerializeField] private Vector3 target;

    // Checkpoint variables
    [SerializeField] private int index; // Which checkpoint the guard starts moving toward
    [SerializeField] private List<GameObject> checkpoints; // List of checkpoints populated in the editor

    // Path tracking for player tracking
    private bool pathtracking;
    [SerializeField] private float tracktimer;
    private Stack<Vector3> trail;
    [SerializeField] private GameObject breadCrumb;

    // Variables used to control the stun feature
    private bool active = true;
    public float stunTime;
    private float stunTimeLeft;


    void Start(){
        trail = new Stack<Vector3>();
        pathtracking = false;
        tracktimer = 0;
        lockedOnPlayer = false; // We never start locked onto a player
        stunTimeLeft = stunTime; // Resets the stun timer
        self = gameObject.GetComponent<Rigidbody>(); // Populates self variable
        if (checkpoints.Count != 0){
            // Initializes the destination to be the ith checkpoint in the list
            Debug.Log("Setting destination on checkpoints");
            destination = checkpoints[index % checkpoints.Count].transform.position; 
        } else {
            // If There is no checkpoint then we use the original location of the guard as its destination
            destination = transform.position;
        }

    }
    void Update()
    {
        timers();
    }
    void FixedUpdate(){
        if(active){
            //hover();
            move();
        }
    }
    private void move(){
        if(active){
            if (lockedOnPlayer){
                // Debug.LogFormat("Locked On to Player");
                target = player.transform.position;
                moveTowardTarget(target);
            } else {
                // Debug.Log("Moving to checkpoint");
                target = destination;
                moveTowardTarget(target);
            }
        }
    }


    private void moveTowardTarget(Vector3 target){
        direction = Vector3.Normalize(target - transform.position);
        if (Vector3.Distance(transform.position, target) > 1){
            // Orients the rigidbody towards its current velocity
            transform.LookAt(transform.position + self.velocity);
            // Debug.Log("Adding force toward Target");
            self.AddForce(direction * acceleration, ForceMode.Acceleration);
        } else {
            self.velocity = self.velocity * (float)0.5;
        }
        if (self.velocity.magnitude > maxVelocity){
            Debug.LogFormat("Velocity: {0}\nMax Velocity: {1}", self.velocity.magnitude, maxVelocity);
            self.velocity = Vector3.Normalize(self.velocity) * maxVelocity;
        }
    }



    public void nextCheckpoint(){
        Debug.Log("CheckPoint Reached");
        index = (index + 1) % checkpoints.Count;
        destination = checkpoints[index].transform.position;
    }

    public void stun(){
        Debug.Log("Stun called");
        if (active){
            active = false;
            stunTimeLeft = stunTime;
        }
    }
    private void countDown(){
        stunTimeLeft -= Time.deltaTime;
        if (stunTimeLeft < 0){
            stunTimeLeft = stunTime;
            active = true;
        }
    }

    public void lockOnPlayer(GameObject detectedPlayer){
        Debug.Log("Locked on Player");
        player = detectedPlayer;
        pathtracking = true;
        lockedOnPlayer = true;
    }

    public void loseLock(){
        lockedOnPlayer = false;
        player = null;
    }

    public void trackpath(){
        if(trail.Count > 2){
            Vector3 temp = trail.Pop();
            Vector3 secondFromTop = trail.Peek();
            // Check if there is a valid path from current position to secondFromTop
            if(viablePath(secondFromTop)){
                trail.Push(transform.position);
            } else {
                trail.Push(temp);
                trail.Push(transform.position);
            }
        } else{
            trail.Push(transform.position);
        }
    }

    private bool viablePath(Vector3 trailPoint){
        RaycastHit hit;

        if(Physics.Raycast(transform.position, trailPoint - transform.position,out hit, Vector3.Distance(transform.position, trailPoint))){
            if (Vector3.Distance(trailPoint, transform.position) < 0.5){
                // Check a few more points to ensure there is a floor between the two points
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
    private void timers(){
        if (!active){countDown();}
        tracktimer += Time.deltaTime;
        if (tracktimer > 1){
            tracktimer = 0;
            if (lockedOnPlayer){
                Instantiate(breadCrumb, transform.position, Quaternion.identity);
                trackpath();
            }
        }

    }
}
