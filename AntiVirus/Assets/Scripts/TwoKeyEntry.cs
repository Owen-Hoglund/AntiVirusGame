using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoKeyEntry : MonoBehaviour
{
    public GameObject secondKey;
    public GameObject twoKeyLock;
    public string function;
    public bool playerInRange;
    public bool keyOne = false;
    public bool keyTwo = false;

    public float unlockTime;
    private float timeLeft;

    void OnTriggerStay(Collider collider){
        if (collider.tag == "Player" || collider.tag == "playerGuard"){
            if (playerInRange == false){
                playerInRange = true;
            }
        }
    }

    void OnTriggerExit(Collider collider){
        if (collider.tag == "Player" || collider.tag == "playerGuard"){
            playerInRange = false;    
        }
    }

    void Update(){
        if (keyOne){
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0){
                keyOne = false;
                secondKey.SendMessage("SecondKeyEntered");
            }
        }
        if (playerInRange){
            if(Input.GetKeyDown("e")){
                if (keyTwo){
                    twoKeyLock.SendMessage(function);
                } else {
                    secondKey.SendMessage("SecondKeyEntered");
                    keyOne = true;
                    timeLeft = unlockTime;
                }
            }
        }
    }
    
    public void SecondKeyEntered(){
        keyTwo = !keyTwo;
    }
}
