using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject deathExplosion;
    private bool isRight;
    private int num;
    private int cur; 
    private float stepSize;
    private float timeToMove;
    private float timeNow;
    private int moveState;
    public float point;
    public float fireOdds;
    public float firePeriod;
    public float fireNow; 
    public GameObject bullet;
    public AudioClip deathSound;
    public AudioClip moveSound1; 
    public AudioClip moveSound2;    
    public AudioClip moveSound3;
    public AudioClip moveSound4;
    Global globalObj;
    public int level;
    public int howlong;
    // Start is called before the first frame update
    public virtual void Start()
    {
        num = 20;
        cur = 10; 
        stepSize = 0.35f;
        timeNow = 0;
        timeToMove = 0.8f;
        point = 10.0f;
        fireOdds = 0.02f;
        firePeriod = 1.0f;
        fireNow = 0; 
        isRight = true;
        moveState = 0;
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        level = globalObj.curLevel;
        howlong = 0; 
    }

    // Update is called once per frame
    public virtual void Update()
    {
        timeToMove = Mathf.Max(0.8f - howlong * 0.05f - level * 0.1f, 0.1f);
    }

    public virtual void FixedUpdate()
    {
        timeNow += Time.deltaTime; 
        fireNow += Time.deltaTime;
        if (timeNow > timeToMove)
        {
            timeNow = 0; 
            Move(); 
            switch (moveState)
            {
                case 0: 
                    AudioSource.PlayClipAtPoint(moveSound1, gameObject.transform.position, 0.1f);
                    break;
                case 1:
                    AudioSource.PlayClipAtPoint(moveSound2, gameObject.transform.position, 0.1f);
                    break;
                case 2:
                    AudioSource.PlayClipAtPoint(moveSound3, gameObject.transform.position, 0.1f);
                    break;
                case 3:
                    AudioSource.PlayClipAtPoint(moveSound4, gameObject.transform.position, 0.1f);
                    break;
            }
            moveState = (moveState + 1) % 4; 
        }

        // fire projectiles by chance 
        if (fireNow > firePeriod)
        {
            if (Random.Range(0.0f, 1.0f) < fireOdds)
            {
                Fire();
            }
            fireNow = 0; 
        }
    }

    void Move()
    {
        if (isRight)
        {
            if (cur > 0)
            {
                cur--;
                gameObject.transform.position += new Vector3(stepSize, 0, 0); 
            } else
            {
                cur = num;
                howlong++;
                isRight = false;
                gameObject.transform.position -= new Vector3(0, 0, stepSize); 
            }
        }
        else
        {
            if (cur > 0)
            {
                cur--;
                gameObject.transform.position -= new Vector3(stepSize, 0, 0); 
            } else
            {
                cur = num;
                howlong++;
                isRight = true;
                gameObject.transform.position -= new Vector3(0, 0, stepSize);
            }
        }
    }

    void Fire()
    {
        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.z -= 1.0f;
        GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
        // get the Bullet Script Component of the new Bullet instance
        BulletScript b = obj.GetComponent<BulletScript>();
        // set the direction the Bullet will travel in
        b.heading = Quaternion.identity;
        b.fromAlien = true;
        obj.layer = 7;
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Bullets"))
        {
            BulletScript b = collider.gameObject.GetComponent<BulletScript>();
            b.Die();
            Die();
            
        }
    }
    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        GameObject glo = GameObject.Find("GlobalObject"); 
        Global g = glo.GetComponent<Global>();
        g.alienTotal--;
        g.score += point; 
        Destroy(gameObject);
    }
}
