using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // Needed for moving character
    public Transform center;

    // These are needed for wall animation
    public GameObject wall1;
    public Transform destination1;
    public GameObject wall2;
    public Transform destination2;
    public GameObject wall3;
    public Transform destination3;
    public GameObject wall4;
    public Transform destination4;

    private bool rotating = false;
    private bool closing = false;
    private bool lifting;
    private float rotation = 0f;
    private float moved = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l")){
            begin();
        }
        if (rotating) {
            rotate();
        }
        if (closing) {
            close();
        }
        if (lifting){
            lift();
        }
    }

    private void rotate(){
        rotation += Time.deltaTime * 9;
        if (rotation > 90){
            rotation = 90;
            rotating = false;
            closing = true;
        }
        Vector3 newRotation = new Vector3(rotation, 0, 0);
        Vector3 newRotation2 = new Vector3(0, 0, rotation);

        wall1.transform.eulerAngles = newRotation2;
        wall2.transform.eulerAngles = newRotation;
        wall3.transform.eulerAngles = -newRotation2;
        wall4.transform.eulerAngles = -newRotation;
        wall1.transform.position += new Vector3(0, (1.5f / 10f) * Time.deltaTime, 0);
        wall2.transform.position += new Vector3(0, (1.5f / 10f) * Time.deltaTime, 0);
        wall3.transform.position += new Vector3(0, (1.5f / 10f) * Time.deltaTime, 0);
        wall4.transform.position += new Vector3(0, (1.5f / 10f) * Time.deltaTime, 0);
    }
    private void close(){
        moved += Time.deltaTime;
        if(moved > 1.5){
            closing = false;
            lifting = true;
        }
        // wall1.transform.position += new Vector3(-Time.deltaTime, 0, 0);
        // wall2.transform.position += new Vector3(0, 0, Time.deltaTime);
        // wall3.transform.position += new Vector3(Time.deltaTime, 0, 0);
        // wall4.transform.position += new Vector3(0, 0, -Time.deltaTime);
        wall1.transform.position = Vector3.MoveTowards(wall1.transform.position, destination1.position, Time.deltaTime);
        wall2.transform.position = Vector3.MoveTowards(wall2.transform.position, destination2.position, Time.deltaTime);
        wall3.transform.position = Vector3.MoveTowards(wall3.transform.position, destination3.position, Time.deltaTime);
        wall4.transform.position = Vector3.MoveTowards(wall4.transform.position, destination4.position, Time.deltaTime);

    }

    private void lift(){
        transform.position += Vector3.up * Time.deltaTime;
    }

    private void begin(){
        rotating = true;
    }
}
