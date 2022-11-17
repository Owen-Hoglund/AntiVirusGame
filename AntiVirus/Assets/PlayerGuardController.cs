using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardController : MonoBehaviour
{
    public Camera cam;
    public float hoverHeight;
    public float hoverForce;
    public float speed;

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
        hover();
    }
    private void move(){
        // Forward / backward movement
        if (Input.GetKey("w")){
            transform.position = transform.position + cam.transform.forward * speed / 100;
        } else if (Input.GetKey("s")){
            transform.position = transform.position - cam.transform.forward * speed / 100;
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20)){
            float height = gameObject.transform.position.y - hit.transform.position.y;
            if (height < hoverHeight){
                rb.AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
            }
            if (height > hoverHeight && gameObject.GetComponent<Rigidbody>().velocity.y > 0){
                rb.AddForce(Vector3.down * hoverForce * 0.25f, ForceMode.Acceleration);
            }
        }
    }

}
