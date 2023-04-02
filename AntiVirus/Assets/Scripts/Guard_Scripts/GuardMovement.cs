using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float maxVelocity;
    private Rigidbody self;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 target;
    private Vector3 direction;
    private bool isTargeting; // Whether the guard is currently targeting something
    private TargetManager targeter;
    
    private void Awake(){
        targeter = gameObject.GetComponent<TargetManager>();
        self = transform.GetComponentInParent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null){
            target = player.transform.position;
        }

        if (isTargeting || player != null){
            moveTowardTarget();
        } else {
            slow();
        }
    }

    private void moveTowardTarget(){
        // Calculates the direction the guard should be moving in
        direction = Vector3.Normalize(target - transform.position);

        // orients the guard towards its velocity (faces the way its going)
        lookTowardsDirection();

        float d = Vector3.Distance(transform.position, target); // Distance to target

        // This checks if we have reached our destination and if so requests a new target from the target manager
        // It will NOT request a new target if we are tracking a player.
        if (d > 1 || player != null){
            // Applies a force in the direction of the target
            self.AddForce(direction * acceleration, ForceMode.Acceleration);
        } else {
            // Debug.Log("Guard has Reached Destination - Requesting Target from Target Manager");
            target = targeter.targetRequest();
        }

        Vector3 xzVel = new Vector3(self.velocity.x, 0, self.velocity.z);
        // This caps velocity
        if (xzVel.magnitude > maxVelocity){
            // Debug.LogFormat("Velocity: {0}\nMax Velocity: {1}", self.velocity.magnitude, maxVelocity);
            xzVel = Vector3.Normalize(xzVel);
            self.velocity = new Vector3(xzVel.x, self.velocity.y, xzVel.z);
        }
    }

    private void lookTowardsDirection(){
        if (self.velocity.magnitude > 0.5){
            Vector3 xzVel = new Vector3(self.velocity.x, 0, self.velocity.z);
            transform.LookAt(transform.position + xzVel);
        }
    }

    private void slow(){
        self.velocity = self.velocity * (float)0.9;
    }




    public void setTarget(Vector3 x){
        target = x;
        isTargeting = true;
    }

    public void setTargeting(bool b){
        isTargeting = b;
    }

    public void setPlayer(GameObject p){
        Debug.Log("Message Received from Target Manager to Lock On Player");
        if (player == null){
            isTargeting = true;
            player = p;
            Debug.Log("Successfully Locked Target to Player");
        } else {
            Debug.Log("Guard is already locked on to a Player");
        }
    }
    public void LosePlayer(){
        player = null;
        target = targeter.targetRequest();
    }
}
