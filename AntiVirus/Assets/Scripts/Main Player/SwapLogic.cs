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
    public float timeRemaining;
    private bool swapped = false;

    void Start(){
        timeRemaining = swaptime;
    }
    public void swap(GameObject guard){
        Debug.Log("Swap initiated");
        realGuard = guard;
        mainPlayer.GetComponentInChildren<Camera>().enabled = false;
        mainPlayer.GetComponent<PlayerController>().enabled = false;
        
        Vector3 location = realGuard.transform.position;
        guard.gameObject.SetActive(false);
        currentDecoy = Instantiate(decoyGuard, location, decoyGuard.transform.rotation);
        swapped = true;
    }

    private void swapBack(){
        currentDecoy.SetActive(false);
        realGuard.SetActive(true);
        mainPlayer.GetComponentInChildren<Camera>().enabled = true;
        mainPlayer.GetComponent<PlayerController>().enabled = true;
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

    public float getTime(){
        return timeRemaining;
    }
}
