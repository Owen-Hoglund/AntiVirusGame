using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomVirusMovement : MonoBehaviour
{
    public float maxVelocity;
    public float randomness;
    private float timer;
    private Rigidbody rb;
    void Start()
    {
        rb = gameObject.transform.GetComponent<Rigidbody>();
        int randx = Random.Range(-1, 1);
        int randy = Random.Range(-1, 1);
        int randz = Random.Range(-1, 1);
        rb.velocity = new Vector3(randx, randy, randz) * maxVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        countDown();
    }
    private void countDown(){
        timer -= Time.deltaTime;
        if (timer < 0){
            randomDirection();
            timer = Random.Range(0, randomness);
        }
    }

    private void randomDirection(){
        int randx = Random.Range(-1, 2);
        int randy = Random.Range(-1, 2);
        int randz = Random.Range(-1, 2);
        // Debug.LogFormat("{0} {1} {2}", randx, randy, randz);
        rb.velocity = new Vector3(randx, randy, randz) * Random.Range(0, maxVelocity);        
    }
}
