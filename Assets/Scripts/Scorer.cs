using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    public Global g;
    public float scoreMultiplier;
    public AudioClip hitSound;
    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.Find("GlobalObject").GetComponent<Global>();
    }

    void OnTriggerEnter(Collider collision)
    {
        Collider collider = collision.GetComponent<Collider>();
        g.score += 300;
        AudioSource.PlayClipAtPoint(hitSound, gameObject.transform.position);
        Destroy(collider.gameObject);

    }
}
