using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardController : MonoBehaviour
{

    public float hoverHeight;
    public float hoverForce;
    public float maxVelocity;
    public List<GameObject> checkPoints;
    private Vector3 destination;
    private GameObject self;
    private Rigidbody selfRigidBody;
    void Start(){
        self = gameObject;
        selfRigidBody = self.GetComponent<Rigidbody>();
        if (checkPoints.Count != 0){
            destination = new Vector3(checkPoints[0].transform.position.x, hoverHeight, checkPoints[0].transform.position.x);
        } else {
            destination = new Vector3(gameObject.transform.position.x, hoverHeight, gameObject.transform.position.x);
        }
    }
    void Update()
    {
        hover();
        move();
    }
    private void hover(){
        RaycastHit hit;
        if (Physics.Raycast(self.transform.position, Vector3.down, out hit, 20)){
            float height = gameObject.transform.position.y - hit.transform.position.y;
            if (height < hoverHeight - 0.1){
                selfRigidBody.AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
            }
            if (height > hoverHeight + 0.1 && gameObject.GetComponent<Rigidbody>().velocity.y > 0){
                selfRigidBody.AddForce(Vector3.down * hoverForce * 0.25f, ForceMode.Acceleration);
            }
        }
    }

    private void move(){

    }
}
