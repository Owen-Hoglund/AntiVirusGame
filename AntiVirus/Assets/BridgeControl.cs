using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeControl : MonoBehaviour
{
    public float length;
    private float remainder;
    public float speed;
    private float trueSpeed;
    private bool building = false;
    private bool built = false;

    // Start is called before the first frame update
    void Start()
    {
        remainder = length;
        trueSpeed = speed / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b")){
            extendOrRetract();
        }
        if (building){
            extendAndRetract();
        }
    }

    private void extendAndRetract(){
        if (built){
            transform.position -= new Vector3(0, 0, trueSpeed / 2);
            transform.localScale -= new Vector3(0, 0, trueSpeed);
            remainder -= trueSpeed;
        } else {
            transform.position += new Vector3(0, 0, trueSpeed / 2);
            transform.localScale += new Vector3(0, 0, trueSpeed);
            remainder -= trueSpeed;
        }

        if (remainder < 0){
            if (built){
                // This just disables the box collider for a second to avoid introducing bugs
                // related to having zero/negative scale
                gameObject.GetComponent<BoxCollider>().enabled = !gameObject.GetComponent<BoxCollider>().enabled;
            }
            building = !building;
            remainder = length;
            built = !built;
        }
    }

    public void extendOrRetract(){
        building = !building;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
