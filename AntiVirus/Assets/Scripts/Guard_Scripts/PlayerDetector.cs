using UnityEngine;
using System.Threading.Tasks; 


public class PlayerDetector : MonoBehaviour
{
    private TargetManager targeter;
    [SerializeField] private GameObject player;
    [SerializeField] private float trackingRange; // How far the guard can see AFTER the player was detected in its visible range
    [SerializeField] private float timeToLosePlayer;
    [SerializeField] private Transform rayOrigin;
    private bool tracking = false; // Whether we are currently tracking a player
    private float time = 0;

    private Vector3 origin;
    private Vector3 direction;

    private void Awake(){
        targeter = gameObject.GetComponentInParent<TargetManager>();
    }
    void FixedUpdate(){
        if (tracking){
            timer();
        }
    }
    private void timer(){
        time += Time.deltaTime; // Add time that has passed to timer
        if(track()){
            time = 0;
        }
        if(time > timeToLosePlayer){
            tracking = false;
            time = 0;
            targeter.LoseLock();
        }
    }

    /*
        Casts a Ray at any new collider within its field of vision.
        Checks if the parent object is a player, and if so locks on to the player.
        Using Raycasts rather than simple collider ensures that lockOnPlayer only
        triggers if the player is actually in the "Vision" of the guard.
    */
    void OnTriggerEnter(Collider collider){
        if(!tracking){ // We dont want to bother checking collisions if we are already tracking a player  
            RaycastHit hit;
            origin = transform.parent.transform.position;
            direction = collider.transform.position - transform.parent.transform.position;
            if (Physics.Raycast(origin, direction, out hit, Vector3.Distance(origin, collider.transform.position))){
                Debug.DrawRay(transform.parent.transform.position, (collider.transform.position - transform.parent.transform.position), Color.magenta, 1);
                if (hit.transform.tag == "Player"){
                    Debug.Log("Sending message to Target Manager that a player was detected");
                    player = hit.transform.gameObject;
                    targeter.LockOnPlayer(player); // Tells  the target manager we are tracking a player

                    time = 0;
                    tracking = true;
                }
            }
        }    
    }

    private bool track(){
        // For easier reading in the following lines
        origin = transform.parent.transform.position;
        direction = Vector3.Normalize(player.transform.position - transform.parent.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, trackingRange)){
            // hit
            if (hit.transform.tag == "Player"){
                Debug.DrawRay(origin, direction * hit.distance, Color.green, 1);
                return true;
            } else {
                Debug.DrawRay(origin, direction * hit.distance, Color.red, 1);
                return false;
            }
        }
        else {
            // hit nothing
            Debug.DrawRay(origin, direction * trackingRange, Color.red, 1);
            return false;
        }
    }

}
