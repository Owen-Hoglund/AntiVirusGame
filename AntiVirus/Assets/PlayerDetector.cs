using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerDetector : MonoBehaviour
{
    private GameObject player;
    private bool playerInSight;
    private int timer; // Number of seconds since the guard last saw the player
    [SerializeField] private int timeUntilLost; // How long a player must be out of sight before the guard loses track
    [SerializeField] private float trackingRange; // How far the guard can see AFTER the player was detected in its visible range
    
    Action onClickDelegate;
    void Start(){
        playerInSight = false;
    }

    /*
        Casts a Ray at any new collider within its field of vision.
        Checks if the parent object is a player, and if so locks on to the player.
        Using Raycasts rather than simple collider ensures that lockOnPlayer only
        triggers if the player is actually in the "Vision" of the guard.
    */
    void OnTriggerEnter(Collider collider){
        if(!playerInSight){    
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.transform.position, collider.transform.position - transform.parent.transform.position, out hit, 8)){
            // Debug.DrawRay(transform.parent.transform.position, (collider.transform.position - transform.parent.transform.position), Color.green, 1);
            if (hit.transform.tag == "Player"){
                gameObject.GetComponentInParent<guardController>().lockOnPlayer(collider.gameObject);
                player = hit.transform.gameObject;
                playerInSight = true;
                tracking();
            }
        }
}    
    }

    private async void tracking(){
        while(playerInSight){
            // For easier reading in the following lines
            Vector3 origin = transform.parent.transform.position;
            Vector3 direction = Vector3.Normalize(player.transform.position - transform.parent.transform.position);


            RaycastHit hit;
            Debug.DrawRay(origin, direction * trackingRange, Color.green, 1);
            if (Physics.Raycast(origin, direction, out hit, trackingRange)){
                // Debug.LogFormat("Distance to hit: {0} \nHit Tag: {1}", hit.distance, hit.transform.tag);
                // Increments the timer if the guard cant see the player due to obstruction
                if (hit.transform.tag != "Player"){
                    timer++;
                } else {
                    timer = 0;
                }
            } else {timer++;} // Increments the timer in the case that the ray hits nothing (common on large flat areas)

            // Waits one second so that we arent needlessly casting rays around
            await Task.Run(() => System.Threading.Thread.Sleep(1000));
            if (timer == timeUntilLost){
                playerInSight = false;
            }
            // Debug.LogFormat("\nTime since last seen: {0} \nTime until Lost: {1}", timer, timeUntilLost - timer);
        }
    }

}
