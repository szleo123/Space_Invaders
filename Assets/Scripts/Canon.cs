using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public Vector3 moveSpeed;
    public GameObject bullet;
    private float firePeriod;
    private const float fireCooldown=0.6f;
    private Vector3 originInScreenCoords;
    private Vector3 botLeft; 
    private Vector3 botRight; 
    private Vector3 botCenter;
    public GameObject deathExplosion;
    public AudioClip shootSound;
    public GameObject FPC;
    public GameObject mainCamera;
    private float boarderLeft;
    private float boarderRight;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed.x = 0.2f;
        firePeriod = 0.0f;
        GameObject glo = GameObject.Find("GlobalObject");
        Global g = glo.GetComponent<Global>();
        originInScreenCoords = g.originInScreenCoords;
        botLeft = new Vector3(0.05f, 0.08f, originInScreenCoords.z);
        botCenter = new Vector3(0.5f, 0.08f, originInScreenCoords.z);
        botRight = new Vector3(0.95f, 0.08f, originInScreenCoords.z); 
        gameObject.transform.position = Camera.main.ViewportToWorldPoint(botCenter);
        boarderRight = Camera.main.ViewportToWorldPoint(botRight).x;
        boarderLeft = Camera.main.ViewportToWorldPoint(botLeft).x;
        FPC = g.FPC;
        mainCamera = g.mainCamera;
        FPC.transform.SetParent(gameObject.transform);
        FPC.transform.localPosition = new Vector3(0, 0.46f, -0.26f);
        FPC.transform.localRotation = Quaternion.identity;
        mainCamera = GameObject.Find("Main Camera");
        FPC.SetActive(false);
        mainCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && firePeriod > fireCooldown)
        {
            // reset the firePeriod 
            firePeriod = 0.0f;
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 1.5f;
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
            // get the Bullet Script Component of the new Bullet instance
            BulletScript b = obj.GetComponent<BulletScript>();
            // set the direction the Bullet will travel in
            b.heading = Quaternion.identity;

            AudioSource.PlayClipAtPoint(shootSound, gameObject.transform.position);
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (FPC.activeSelf)
            {
                FPC.SetActive(false);
                mainCamera.SetActive(true);
            }else
            {
                mainCamera.SetActive(false);
                FPC.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {
        firePeriod += Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, -45));
            GetComponent<Rigidbody>().MoveRotation(rot);
            if (gameObject.transform.position.x <= boarderRight)
            {
                gameObject.transform.position += moveSpeed;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 45));
            GetComponent<Rigidbody>().MoveRotation(rot);
            if (gameObject.transform.position.x >= boarderLeft)
            {
                gameObject.transform.position -= moveSpeed;
            }
        }
        else
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 0));
            GetComponent<Rigidbody>().MoveRotation(rot);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Bullets"))
        {
            BulletScript b = collider.gameObject.GetComponent<BulletScript>();
            b.Die();
        }
        Die(); 
    }

    public void Die()
    {
        GameObject glo = GameObject.Find("GlobalObject");
        Global g = glo.GetComponent<Global>();
        g.deathTrigger = true;
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        FPC.SetActive(false);
        mainCamera.SetActive(true);
        FPC.transform.SetParent(null);
        Destroy(gameObject);
    }
}
