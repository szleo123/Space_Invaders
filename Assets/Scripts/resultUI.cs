using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resultUI : MonoBehaviour
{
    TextMeshProUGUI resultText;
    // Start is called before the first frame update
    void Start()
    {

        resultText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        if (Global.win)
        {
            resultText.text = "YOU WIN";
        }else
        {
            resultText.text = "GAME OVER";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
