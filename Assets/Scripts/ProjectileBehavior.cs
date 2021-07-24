using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    // Adjustable Parameters
    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody ourRigidbody = GetComponent<Rigidbody>();
        ourRigidbody.velocity = transform.forward * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
