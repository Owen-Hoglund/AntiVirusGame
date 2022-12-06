using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    private Rigidbody self;
    private GameObject player;
    [SerializeField] private float acceleration;
    [SerializeField] private float hoverHeight;
    [SerializeField] private float maxVelocity;
    [SerializeField] private Vector3 target; // when im done I need to get rid of the serialization here
    private Vector3 direction;
    private bool isTargeting; // Whether the guard is currently targeting something
    private bool active;
    


    void Start()
    {
        self = transform.GetComponent<Rigidbody>();
        active = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null){target = player.transform.position;}
        if(active){
            hover();
            if(isTargeting){moveTowardTarget();} else{slow();}
        }

    }

    private void moveTowardTarget(){
        direction = Vector3.Normalize(target - transform.position);
        if (Vector3.Distance(transform.position, target) > 1){
            // Orients the rigidbody towards its current velocity
            Vector3 xzVel = new Vector3(self.velocity.x, 0, self.velocity.z);
            transform.LookAt(transform.position + xzVel);

            // Applies a force in the direction of the target
            self.AddForce(direction * acceleration, ForceMode.Acceleration);
        } else {
            Debug.Log("Guard has Reached Destination - Requesting Target from Target Manager");
            target = gameObject.GetComponent<TargetManager>().targetRequest();
        }
        if (self.velocity.magnitude > maxVelocity){
            Debug.LogFormat("Velocity: {0}\nMax Velocity: {1}", self.velocity.magnitude, maxVelocity);
            self.velocity = Vector3.Normalize(self.velocity) * maxVelocity;
        }
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
        target = gameObject.GetComponent<TargetManager>().targetRequest();
    }

    private void slow(){
        self.velocity = self.velocity * (float)0.9;
    }

    private void hover(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20)){
            // Finds height above closest gameObject below
            float height = gameObject.transform.position.y - hit.transform.position.y;

            // If we are below our chosen height we accelerate vertically
            if (height < hoverHeight - 0.1){
                self.velocity += Vector3.up * 13f * Time.deltaTime;
            }

            // If we are above our height AND our vertical velocity is still positive we apply a downward force 
            if (height > hoverHeight + 0.1 && gameObject.GetComponent<Rigidbody>().velocity.y > 0){
                self.velocity += Vector3.down * Time.deltaTime;
            }

            // This balances force of gravity within a small range of our height
            if (Mathf.Abs(height - hoverHeight) < 0.1 && self.velocity.y < 1){
                self.velocity += Vector3.up * 9.811f * Time.deltaTime;
            }
        }
    }
}
