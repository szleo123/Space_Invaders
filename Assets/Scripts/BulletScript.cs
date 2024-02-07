using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion heading;
    private Vector3 originInScreenCoords;
    public bool fromAlien;
    public Camera mainCamera;
    public float upper;
    public float lower;
    public bool dead;
    public Rigidbody rb; 
    // Start is called before the first frame update
    void Start()
    {
        GameObject glo = GameObject.Find("GlobalObject");
        Global g = glo.GetComponent<Global>();
        dead = false;
        originInScreenCoords = g.originInScreenCoords;
        upper = g.screenUpper; 
        lower = g.screenLower;
        thrust.z = 1000.0f;
        // do not passively decelerate
        rb = GetComponent<Rigidbody>();
        rb.drag = 0;
        // set the direction it will travel in
        rb.MoveRotation(heading);
        // apply thrust once, no need to apply it again since
        // it will not decelerate
        if (fromAlien)
        {
            thrust.z = -400.0f; 
        }
        rb.AddRelativeForce(thrust);
    }

    // Update is called once per frame
   void Update()
    {
        if ((gameObject.transform.position.z >= upper) ||
            (gameObject.transform.position.z <= lower))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // make sure the player is fixed in 2 directions 
        gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        if (dead)
        {
            float mass = GetComponent<Rigidbody>().mass;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -9.8f * mass));
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Platform"))
        {
            fromAlien = false;
        }
        Die(); 
    }

    public void Die()
    {
        dead = true;
    }
}
