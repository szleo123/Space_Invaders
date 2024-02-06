using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    // Start is called before the first frame update
    Global globalObj;
    TextMeshProUGUI livesText;
    // Use this for initialization
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        livesText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + globalObj.lives;
    }
}
