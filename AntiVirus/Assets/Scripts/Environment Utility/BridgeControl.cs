using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeControl : MonoBehaviour
{
    public float length;
    private float remainder;
    public float speed;
    private bool building = false;
    private bool built = false;

    // Start is called before the first frame update
    void Start()
    {
        remainder = length;
    }

    // Update is called once per frame
    void Update()
    {
        if (building){
            extendAndRetract();
        }
    }

    private void extendAndRetract(){
        if (built){
            transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
            transform.localScale -= new Vector3(0, 0, speed * Time.deltaTime);
            remainder -= speed * Time.deltaTime;
        } else {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime / 2);
            transform.localScale += new Vector3(0, 0, speed * Time.deltaTime);
            remainder -= speed * Time.deltaTime;
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
        if (building == false){
            gameObject.GetComponent<BoxCollider>().enabled = true;
            building = true;
        }
    }
}
