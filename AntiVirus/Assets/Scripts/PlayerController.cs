using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float hoverHeight;
    public float maxVelocity;

    private Rigidbody rb;

    void Awake(){
        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update(){
        if(!rb.freezeRotation){rb.freezeRotation = true;}
        move();
    }
    void FixedUpdate(){
        //hover();
    }
    private void move(){
        // Forward / backward movement
        if (Input.GetKey("w")){
            rb.velocity += new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * maxVelocity;
        } 
        else if (Input.GetKey("s")){
            rb.velocity += new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * -maxVelocity;
        }

        // Left / Right movement
        if (Input.GetKey("a")){
            rb.velocity += (-cam.transform.right) * maxVelocity;
        } else if (Input.GetKey("d")){
            rb.velocity += (cam.transform.right) * maxVelocity;
        }
        // Rapid slowdown if not actively accelerating
        if (!Input.GetKey("w")  && !Input.GetKey("a") &&!Input.GetKey("s")  && !Input.GetKey("d") && rb.velocity.magnitude > 0.1){
            rb.velocity = new Vector3(rb.velocity.x * 0.1f, rb.velocity.y, rb.velocity.z * 0.1f);
        }

        // Cap our velocity by max speed
        float xzVelocity = Mathf.Sqrt((rb.velocity.x) * (rb.velocity.x) + (rb.velocity.z) * (rb.velocity.z));
        if(xzVelocity > maxVelocity){
            rb.velocity = Vector3.Normalize(rb.velocity) * maxVelocity;
        }

    }

    // Keeps the player hovering over whatever object is below it
    private void hover(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20)){
            // Finds height above closest gameObject below
            float height = gameObject.transform.position.y - hit.transform.position.y;

            // If we are below our chosen height we accelerate vertically
            if (height < hoverHeight - 0.1){
                rb.velocity += Vector3.up * 13f * Time.deltaTime;
            }

            // If we are above our height AND our vertical velocity is still positive we apply a downward force 
            if (height > hoverHeight + 0.1 && gameObject.GetComponent<Rigidbody>().velocity.y > 0){
                rb.velocity += Vector3.down * Time.deltaTime;
            }

            // This balances force of gravity within a small range of our height
            if (Mathf.Abs(height - hoverHeight) < 0.1 && rb.velocity.y < 1){
                rb.velocity += Vector3.up * 9.811f * Time.deltaTime;
            }
        }
    }

}
