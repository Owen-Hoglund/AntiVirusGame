using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardController : MonoBehaviour
{

    public float hoverHeight;
    public float maxVelocity;
    public int index;
    public List<GameObject> checkpoints;
    public float stunTime;
    private float stunTimeLeft;
    [SerializeField] private bool lockedOnPlayer;
    [SerializeField] private GameObject player;
    private Vector3 direction;

    private Vector3 destination;
    private Rigidbody self;
    private bool active = true;

    void Start(){
        lockedOnPlayer = false;
        stunTimeLeft = stunTime;
        self = gameObject.GetComponent<Rigidbody>();
        if (checkpoints.Count != 0){
            destination = checkpoints[index % checkpoints.Count].transform.position;
        } else {
            destination = transform.position;
        }

    }
    void Update()
    {
        // if (checkpoints.Count != 0 && active){
        //     move();
        // }
        move();
        if (!active){countDown();}
    }
    void FixedUpdate(){
        if(active){hover();}
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

    private void move(){
        Rigidbody self = gameObject.GetComponent<Rigidbody>();
        if (lockedOnPlayer){
            direction = Vector3.Normalize(player.transform.position - transform.position);
        } else{
            direction = Vector3.Normalize(destination - transform.position);
        }
        
        // Accelerates towards the current checkpoint then checks its velocity
        // if the velocity is higher than the max we restrict it to our max velocity while retaining its direction
        if (Vector3.Distance(transform.position, destination) > 3){
            // Orients the rigidbody towards its current velocity
            transform.LookAt(transform.position + self.velocity);
            self.AddForce(direction, ForceMode.Acceleration);
        } else {
            self.velocity = self.velocity * (float)0.5;
        }
        if (self.velocity.magnitude > maxVelocity){
            self.velocity = Vector3.Normalize(self.velocity) * maxVelocity;
        }
    }

    public void nextCheckpoint(){
        //Debug.Log("CheckPoint Reached");
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
        lockedOnPlayer = true;
    }

    public void loseLock(){
        lockedOnPlayer = false;
        player = null;
    }
}
