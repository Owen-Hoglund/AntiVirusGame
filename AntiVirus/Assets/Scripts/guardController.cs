using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardController : MonoBehaviour
{

    public float hoverHeight;
    public float hoverForce;
    public float maxVelocity;
    public float acceleration;
    public int index;
    public List<GameObject> checkpoints;


    private Vector3 destination;
    private Rigidbody self;


    void Start(){
        self = gameObject.GetComponent<Rigidbody>();
        if (checkpoints.Count != 0){
            destination = checkpoints[index % checkpoints.Count].transform.position;
        }

    }
    void Update()
    {
        hover();
        //move();
        if (checkpoints.Count != 0){
            move();
        }

    }
    private void hover(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20)){
            float height = gameObject.transform.position.y - hit.transform.position.y;
            if (height < hoverHeight){
                self.AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
            }
            if (height > hoverHeight && gameObject.GetComponent<Rigidbody>().velocity.y > 0){
                self.AddForce(Vector3.down * hoverForce * 0.25f, ForceMode.Acceleration);
            }
        }
    }

    private void move(){
        Rigidbody self = gameObject.GetComponent<Rigidbody>();
        Vector3 direction = Vector3.Normalize(destination - transform.position);

        // Accelerates towards the current checkpoint then checks its velocity
        // if the velocity is higher than the max we restrict it to our max velocity while retaining its direction
        self.AddForce(direction * acceleration, ForceMode.Acceleration);
        if (self.velocity.magnitude > maxVelocity){
            self.velocity = Vector3.Normalize(self.velocity) * maxVelocity;
        }
    }

    public void nextCheckpoint(){
        //Debug.Log("CheckPoint Reached");
        index = (index + 1) % checkpoints.Count;
        destination = checkpoints[index].transform.position;
    }
}
