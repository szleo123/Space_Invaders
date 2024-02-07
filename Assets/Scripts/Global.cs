using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public GameObject Alien;
    public GameObject Canon;
    public GameObject AlienM;
    public GameObject AlienH;
    public GameObject Bunker;
    public GameObject FPC;
    public GameObject mainCamera;
    public GameObject Platform;
    public GameObject Basket;
    public GameObject Bouncer; 
    public float timer;
    public float score;
    public int lives;
    public bool deathTrigger;
    public float respawnTime;
    public float respawntimer; 
    public Vector3 originInScreenCoords;
    private bool levelClear;
    public int curLevel;
    // Parameters for alien spawning set up 
    private float upper = 0.97f; //0.95
    private float lower = 0.7f; //0.6
    private float left = 0.15f; //0.15
    private float right = 0.85f; //0.85
    /// /////////////////////////////////
    private float alienInRow; 
    private float alienRow;
    public int alienTotal;
    public float screenUpper; 
    public float screenLower;
    public float screenLeft;
    public float screenRight;
    public float goalScore; 

    public static bool win;

    // Start is called before the first frame update
    void Start()
    {
        levelClear = true;
        curLevel = 0;
        timer = 30; 
        score = 0;
        lives = 2;
        deathTrigger = false; 
        respawnTime = 2;
        respawntimer = 0;
        originInScreenCoords = Camera.main.WorldToViewportPoint(new Vector3(0, 0, 0));
        screenUpper = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.0f, originInScreenCoords.z)).z;
        screenLower = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, originInScreenCoords.z)).z;
        alienInRow = 12;
        alienRow = 5;
        goalScore = 500; 
        FPC = GameObject.Find("FPC");
        mainCamera = GameObject.Find("Main Camera");
        Instantiate(Canon, new Vector3(0, 0, 0), Quaternion.identity);
        Vector3 platformPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.04f ,originInScreenCoords.z));
        Instantiate(Platform, platformPos, Quaternion.identity);
        Vector3 basketPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.16f, originInScreenCoords.z));
        Instantiate(Basket, basketPos, Quaternion.identity);
        Vector3 bouncerPos; 
        // remove bunker for beta gameplay
        /*        Instantiate(Bunker, Camera.main.ViewportToWorldPoint(new Vector3(0.2f, 0.3f, originInScreenCoords.z)), Quaternion.identity);
                Instantiate(Bunker, Camera.main.ViewportToWorldPoint(new Vector3(0.4f, 0.3f, originInScreenCoords.z)), Quaternion.identity);
                Instantiate(Bunker, Camera.main.ViewportToWorldPoint(new Vector3(0.6f, 0.3f, originInScreenCoords.z)), Quaternion.identity);
                Instantiate(Bunker, Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.3f, originInScreenCoords.z)), Quaternion.identity);*/
        // initialize the set of bouncers
        float bStart = 0.1f;
        float bEnd = 0.9f; 
        float numBouncerPerRow = 6; 
        float bounceInc = (bEnd - bStart) / numBouncerPerRow;
        float vStart = 0.3f;
        float vInc = 0.08f; 
        for (int i = 0; i <= numBouncerPerRow; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                float tester = 0;
                if (j % 2 == 0) tester = bounceInc / 2; 
                bouncerPos = Camera.main.ViewportToWorldPoint(new Vector3(bStart + bounceInc * i + tester, vStart + j * vInc, originInScreenCoords.z));
                Instantiate(Bouncer, bouncerPos, Quaternion.identity);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (levelClear)
        {
            curLevel++;
            goalScore += 500; 
            levelClear = false;
            SpawnAliens();

        }
        timer -= Time.deltaTime; 
        // handle live decreases 
        if (deathTrigger)
        {
            respawntimer += Time.deltaTime; 
            if (respawntimer > respawnTime)
            {
                respawntimer = 0;
                deathTrigger = false; 
                lives--;
                if (lives >= 0)
                {
                    Instantiate(Canon, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else
                {
                    win = false;
                    SceneManager.LoadScene("resultScene"); 
                }
            }   
        }

        if (alienTotal <=  0)
        {
            levelClear = true;
        }
        if (curLevel == 10)
        {
            win = true;
            SceneManager.LoadScene("resultScene"); 
        }
    }

    private void SpawnAliens()
    {
        float horizontalInc = (right - left) / alienInRow;
        float verticalInc = (upper - lower) / alienRow;
        for (int i = 0; i <= alienInRow; i++)
        {
            for (int j = 0;  j < alienRow; j++)
            {
                alienTotal++; 
                if (j < 2)
                {
                    Instantiate(Alien, Camera.main.ViewportToWorldPoint(new Vector3(left + i * horizontalInc, lower + j * verticalInc, originInScreenCoords.z)), Quaternion.identity);
                } else if(j < 4)
                {
                    Instantiate(AlienM, Camera.main.ViewportToWorldPoint(new Vector3(left + i * horizontalInc, lower + j * verticalInc, originInScreenCoords.z)), Quaternion.identity);
                } else
                {
                    Instantiate(AlienH, Camera.main.ViewportToWorldPoint(new Vector3(left + i * horizontalInc, lower + j * verticalInc, originInScreenCoords.z)), Quaternion.identity);
                }
            }
        }
    }
}
