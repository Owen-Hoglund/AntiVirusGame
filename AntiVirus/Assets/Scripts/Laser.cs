using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject manager;
    public Transform origin; // This is the camera and it is the center of where we will cast our ray from
    public Transform reticle; // This is the reticle and it is what we will use to point our raycast
    public float laserRange;
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Debug.DrawRay(origin.position, (reticle.position - origin.position) * laserRange, Color.red, 1);
            if (Physics.Raycast(origin.position, reticle.position - origin.position, out hit, laserRange)){
                if (hit.transform.tag == "Guard"){
                    Debug.Log("Stunned Guard");
                    hit.transform.SendMessage("stun");
                }
            }
        } else if (Input.GetMouseButtonDown(1)){
            Debug.Log("Fired Swap Laser");
            RaycastHit hit;
            Debug.DrawRay(origin.position, (reticle.position - origin.position) * laserRange, Color.red, 1);
            if (Physics.Raycast(origin.position, reticle.position - origin.position, out hit, laserRange)){
                if (hit.transform.tag == "Guard"){
                    Debug.Log("Hit a guard");
                    manager.SendMessage("swap", hit.transform.gameObject);
                }
            }
            }
        }
}
