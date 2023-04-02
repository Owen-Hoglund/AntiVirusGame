using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPathing : MonoBehaviour
{
    [SerializeField] private float timer = 0;
    [SerializeField] private Stack<Vector3> trail = new Stack<Vector3>();
    [SerializeField] private bool isTracking = false;
    [SerializeField] private int StackSize;

    void Update()
    {
        StackSize = trail.Count;
        timer += Time.deltaTime;
        if (timer > 0.25 && isTracking){
            timer = 0;
            PushLocation();
        }
    }

    private void PushLocation(){
        if (trail.Count > 1){
            Vector3 temp = trail.Pop();
            RaycastHit hit;
            if(Physics.Raycast(transform.position, trail.Peek() - transform.position,
                                out hit, Vector3.Distance(transform.position, trail.Peek())))
            {
                trail.Push(temp);
                trail.Push(transform.position);
            }
            else {
                trail.Push(transform.position);
            }
        } else {
            trail.Push(transform.position);
        }
    }

    public Vector3 Next(){
        return trail.Pop();
    }

    public int GetCount(){
        return trail.Count;
    }
    
    public void Tracking(bool b){
        isTracking = b;
    }
}
