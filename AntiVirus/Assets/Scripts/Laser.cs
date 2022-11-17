using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform origin; // This is the camera and it is the center of where we will cast our ray from
    public Transform reticle; // This is the reticle and it is what we will use to point our raycast
    public float laserRange;
    void Update(){
        if (Input.GetMouseButton(0)){
            Debug.Log("Fired Stun Laser");
            RaycastHit hit;
            if (Physics.Raycast(origin.position, reticle.position - origin.position, out hit, laserRange)){
                Debug.Log("Laser hit something");
                if (hit.transform.tag == "Guard"){
                    Debug.Log("Hit a guard but not doing anything yet");
                }
            }
        } else if (Input.GetMouseButton(1)){
            Debug.Log("Fired Swap Laser");
                RaycastHit hit;
                if (Physics.Raycast(origin.position, reticle.position - origin.position, out hit, laserRange)){
                    Debug.Log("Laser hit something");
                    if (hit.transform.tag == "Guard"){
                        Debug.Log("Hit a guard with Swap but not doing anything yet");
                    }
                }
            }
        }
}
