using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardController : MonoBehaviour
{
    public Camera cam;
    public float hoverHeight;
    public float hoverForce;
    public float speed;
    public float maxVelocity;

    private Rigidbody rb;

    void Awake(){
        Debug.Log("Locking Rotation on Awake");
        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Debug.LogFormat("freezeRotation: {0}", rb.freezeRotation);
    }

    void Update(){
        if(!rb.freezeRotation){rb.freezeRotation = true;}
        move();
        //hover();
    }
    void FixedUpdate(){
        // Debug.Log("Fixed Update");
        hover();
    }
    private void move(){
        // Forward / backward movement
        if (Input.GetKey("w")){
            if (rb.velocity.magnitude < maxVelocity){
                rb.AddForce(new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * 10, ForceMode.Acceleration);
            }
            rb.velocity = Vector3.Normalize(new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z)) * rb.velocity.magnitude;
        } 
        else if (Input.GetKey("s") && rb.velocity.magnitude < maxVelocity){
            rb.AddForce(new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * -10, ForceMode.Acceleration);
        }
        // Rapid slowdown if not actively accelerating
        else if (!Input.GetKey("w")  && !Input.GetKey("s") && rb.velocity.magnitude > 0.1){
            rb.AddForce(new Vector3(rb.velocity.x, 0, rb.velocity.z) * -1 * speed, ForceMode.Acceleration);
        }

        // Left / Right movement
        if (Input.GetKey("a")){
            transform.position = transform.position - cam.transform.right * speed / 100;
        } else if (Input.GetKey("d")){
            transform.position = transform.position + cam.transform.right * speed / 100;
        }
    }

    // Keeps the player hovering over whatever object is below it
    private void hover(){
        Debug.Log("hovering");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20)){
            float height = gameObject.transform.position.y - hit.transform.position.y;
            if (height < hoverHeight - 0.1){

                rb.velocity += Vector3.up * 13f * Time.deltaTime;
            }
            if (height > hoverHeight + 0.1 && gameObject.GetComponent<Rigidbody>().velocity.y > 0){
                rb.AddForce(Vector3.down * hoverForce * 0.25f, ForceMode.Acceleration);
                rb.velocity += Vector3.down * Time.deltaTime;
            }

            // This balances force of gravity within a small range of our height
            if (Mathf.Abs(height - hoverHeight) < 0.1 && rb.velocity.y < 1){
                rb.velocity += Vector3.up * 9.811f * Time.deltaTime;
            }
        }
    }

}
