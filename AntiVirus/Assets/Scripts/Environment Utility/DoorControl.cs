using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public float speed;
    public GameObject PortManager;

    // Private variables
    private bool isOpen;
    private bool underway;
    private float OriginalScale;
    

    void Start(){
        OriginalScale = gameObject.transform.localScale.y;
        underway = false;
        isOpen = false;
    }
    void Update()
    {
        if (underway){move();}
    }
    public void interact(){
        Debug.Log("Received Message");
        if (!underway){
            Debug.Log("Moving Door");
            underway = true;
        }
    }

    private void move(){
        Debug.Log("Moving");
        if (isOpen){
            Debug.Log("Closing");
            close();
        } else {
            Debug.Log("Opening");
            open();
        }
    }

    private void open(){
        gameObject.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        gameObject.transform.localScale -= new Vector3(0, speed * 2 * Time.deltaTime, 0);
        if (gameObject.transform.localScale.y < 0){
            isOpen = true;
            underway = false;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0, gameObject.transform.localScale.z);
            if (PortManager != null){
                PortManager.SendMessage("done");
            }
        }
    }
    private void close(){
        Debug.LogFormat("Current Scale = {0} \n OriginalScale = {1}", gameObject.transform.localScale.y, OriginalScale);
        gameObject.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        gameObject.transform.localScale += new Vector3(0, speed * 2 * Time.deltaTime, 0);
        if (gameObject.transform.localScale.y > OriginalScale){
            isOpen = false;
            underway = false;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, OriginalScale, gameObject.transform.localScale.z);
            if (PortManager != null){
                PortManager.SendMessage("next");
            }
        }
    }

    public bool getOpen(){
        return isOpen;
    }
}
