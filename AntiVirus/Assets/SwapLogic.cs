using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapLogic : MonoBehaviour
{
    public GameObject mainPlayer;
    public GameObject decoyGuard;
    private GameObject currentDecoy;
    private GameObject realGuard;
    public float swaptime;
    private float timeRemaining;
    public bool swapped = false; // CHange to private later


    public void swap(GameObject guard){
        realGuard = guard;
        mainPlayer.GetComponentInChildren<Camera>().enabled = false;
        mainPlayer.GetComponent<PlayerGuardController>().enabled = false;
        
        Vector3 location = realGuard.transform.position;
        guard.gameObject.SetActive(false);
        currentDecoy = Instantiate(decoyGuard, location, decoyGuard.transform.rotation);
        swapped = true;
    }

    private void swapBack(){
        currentDecoy.SetActive(false);
        realGuard.SetActive(true);
        mainPlayer.GetComponentInChildren<Camera>().enabled = true;
        mainPlayer.GetComponent<PlayerGuardController>().enabled = true;
        Destroy(currentDecoy);
    }

    void Update(){
        if (swapped){
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0){
                timeRemaining = swaptime; 
                swapped = false;
                swapBack();
            }
        }
    }
}
