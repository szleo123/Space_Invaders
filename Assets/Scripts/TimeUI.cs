using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    // Start is called before the first frame update
    Global globalObj;
    TextMeshProUGUI timerText;
    // Use this for initialization
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        timerText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        float minute = Mathf.FloorToInt(globalObj.timer / 60);
        float second = Mathf.FloorToInt(globalObj.timer % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minute, second);
    }
}
