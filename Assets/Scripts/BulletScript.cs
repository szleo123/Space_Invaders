using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        GameObject glo = GameObject.Find("GlobalObject");
        Global g = glo.GetComponent<Global>();
        originInScreenCoords = g.originInScreenCoords;
        upper = g.screenUpper; 
        lower = g.screenLower;
        thrust.z = 800.0f;
        // do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;
        // set the direction it will travel in
        GetComponent<Rigidbody>().MoveRotation(heading);
        // apply thrust once, no need to apply it again since
        // it will not decelerate
        if (fromAlien)
        {
            thrust.z = -400.0f; 
        }
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    // Update is called once per frame
   void Update()
    {
        if ((gameObject.transform.position.z >= upper) ||
            (gameObject.transform.position.z <= lower))
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
