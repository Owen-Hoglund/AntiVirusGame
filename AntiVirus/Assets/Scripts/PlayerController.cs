using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float hoverHeight;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float maxVelocity;
    [SerializeField] private Vector3 xzVel;

    private Rigidbody rb;

    void Awake(){
        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update(){
        if (Input.GetKeyDown("space")){
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        }
    }

    
    private void Start(){
        jumpHeight = Mathf.Sqrt((float)2 * (float)9.81 * jumpHeight);
    }

    void FixedUpdate(){
                if(!rb.freezeRotation){rb.freezeRotation = true;}
        if(Input.GetKey("left shift")){
            maxVelocity = sprintSpeed;
        } else {
            maxVelocity = walkSpeed;
        }
        move();
    }
    private void move(){
        // Forward / backward movement
        if (Input.GetKey("w")){
            Vector3 f = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
            rb.AddForce(f * 50, ForceMode.Acceleration);
        } 
        else if (Input.GetKey("s")){
            Vector3 f = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
            rb.AddForce(f * -50, ForceMode.Acceleration);
        }

        // Left / Right movement
        if (Input.GetKey("a")){
            Vector3 f = Vector3.Normalize(cam.transform.right);
            rb.AddForce(f * -50, ForceMode.Acceleration);
        } else if (Input.GetKey("d")){
            Vector3 f = Vector3.Normalize(cam.transform.right);
            rb.AddForce(f * 50, ForceMode.Acceleration);
        }
        // Rapid slowdown if not actively accelerating
        if (!Input.GetKey("w")  && !Input.GetKey("a") &&!Input.GetKey("s")  && !Input.GetKey("d") && rb.velocity.magnitude > 0.1){
            rb.velocity = new Vector3(rb.velocity.x * 0.1f, rb.velocity.y, rb.velocity.z * 0.1f);
        }
        
        // Cap our velocity by max speed
        xzVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if(xzVel.magnitude > maxVelocity){
            xzVel = Vector3.Normalize(xzVel) * maxVelocity;
            rb.velocity = new Vector3(xzVel.x, rb.velocity.y, xzVel.z);
        }

    }

}
