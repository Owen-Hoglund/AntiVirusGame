using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaActivationControl : MonoBehaviour
{
    public GameObject ZoneOne;
    public GameObject ZoneTwo;

    private bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider){
        if (collider.tag == "Player"){
            Debug.Log("Player Entered Zone Control Area");
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider collider){
        if (collider.tag == "Player"){
            Debug.Log("Player Exited Zone Control Area");
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown("e")){
            Debug.Log("Player Initiated Zone Control");

            ZoneOne.SetActive(!ZoneOne.activeSelf);
            ZoneTwo.SetActive(!ZoneTwo.activeSelf);
        }
    }
}
