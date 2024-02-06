using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public AudioClip shieldBreak; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Bullets"))
        {
            BulletScript b = collider.gameObject.GetComponent<BulletScript>();
            b.Die();
        }
        AudioSource.PlayClipAtPoint(shieldBreak, gameObject.transform.position);
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
