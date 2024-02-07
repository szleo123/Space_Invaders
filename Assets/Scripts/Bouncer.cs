using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float bounceForceMin; // Minimum bounce force
    public float bounceForceMax; // Maximum bounce force
    public AudioClip bounceSound;
    // Start is called before the first frame update
    void Start()
    {
        bounceForceMin = 800.0f;
        bounceForceMax = 2000.0f;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        // Get the normal of the collision
        Vector3 collisionNormal = collision.contacts[0].normal; 
        // Create a bounce direction that's primarily away from the bouncer,
        // but with some randomness added perpendicular to the collision normal
        Vector3 randomPerpendicular = Vector3.Cross(collisionNormal, Random.insideUnitSphere).normalized;
        Vector3 bounceDirection = (collisionNormal + randomPerpendicular * 0.3f).normalized; // Adjust the factor to control randomness
        bounceDirection.y = 0;
        bounceDirection.Normalize(); 

        // Apply a random bounce force within the specified range
        float bounceForce = Random.Range(bounceForceMin, bounceForceMax);
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(-bounceDirection * bounceForce);
        AudioSource.PlayClipAtPoint(bounceSound, gameObject.transform.position);

    }
}
