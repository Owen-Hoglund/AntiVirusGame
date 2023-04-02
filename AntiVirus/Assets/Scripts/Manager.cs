using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private float StunTimer;
    private GuardMovement moving;
    private Hover hovering;
    private GuardPathing pathing;
    private TargetManager targeting;
    private PlayerDetector detecting;
    private bool timing;
    private float timer = 0;

    void Update(){
        if (timing){
            timer += Time.deltaTime;
            if(timer > StunTimer){
                timer = 0;
                activate();
            }
        }
    }

    private void Awake(){
        moving = gameObject.GetComponentInChildren<GuardMovement>();
        hovering = gameObject.GetComponentInChildren<Hover>();
        pathing = gameObject.GetComponentInChildren<GuardPathing>();
        targeting = gameObject.GetComponentInChildren<TargetManager>();
        detecting = gameObject.GetComponentInChildren<PlayerDetector>();
    }

    public void stun(){
        Debug.Log("Stunned");
        deactivate();
    }
    private void deactivate(){
        moving.enabled = false;
        hovering.enabled = false;
        pathing.enabled = false;
        targeting.enabled = false;
        detecting.enabled = false;
        timing = true;
    }

    private void activate(){
        moving.enabled = true;
        hovering.enabled = true;
        pathing.enabled = true;
        targeting.enabled = true;
        detecting.enabled = true;
        timing = false;
    }

}
