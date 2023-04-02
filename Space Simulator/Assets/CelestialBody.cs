using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [SerializeField] private Collider[] bodies;
    [SerializeField] private float gravitational_Constant = 2F;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float detection_radius = 2000;
    [SerializeField] private float initial_velocity = 1;
    [SerializeField] private float force_display;
    // Start is called before the first frame update
    void Start()
    {
        int this_layer = transform.gameObject.layer;
        transform.gameObject.layer = LayerMask.NameToLayer("Default");
        bodies = Physics.OverlapSphere(transform.position, detection_radius, LayerMask.GetMask("Celestial Body"));
        rb = transform.GetComponent<Rigidbody>();
        transform.gameObject.layer = this_layer;
        
        
        // Later I should write  a function to create vectors orthogonal to the direction of the planet
        rb.velocity = rb.velocity + Vector3.forward * initial_velocity;
    }

    void FixedUpdate()
    {
        foreach (Collider body in bodies){
            rb.AddForce(gravitational_Force(transform, body.transform), ForceMode.Acceleration);
        }
    }

    private Vector3 gravitational_Force(Transform body_1, Transform body_2){
        float m1 = body_1.GetComponent<Rigidbody>().mass;
        float m2 = body_1.GetComponent<Rigidbody>().mass;

        Vector3 direction = Vector3.Normalize(body_2.position - body_1.position);
        float r_squared = Mathf.Pow(Vector3.Distance(body_1.position, body_2.position), 2);
        float force = gravitational_Constant * m1 * m2 / r_squared;
        return direction * force;
    }
}
