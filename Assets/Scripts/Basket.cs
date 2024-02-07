using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Basket : MonoBehaviour
{
    Rigidbody rb;
    public Global g;
    private float leftLim; 
    private float rightLim;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.1f;
        GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
        rb = GetComponent<Rigidbody>();
        g = GameObject.Find("GlobalObject").GetComponent<Global>();
        leftLim = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, g.originInScreenCoords.z)).x;
        rightLim = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, g.originInScreenCoords.z)).x;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x > rightLim)
        {
            speed = -speed;
        }
        else if (transform.position.x < leftLim)
        {
            speed = -speed;
        }
        transform.position += new Vector3(speed, 0, 0);
    }

}
