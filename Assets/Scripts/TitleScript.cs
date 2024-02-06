using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    private GUIStyle buttonStyle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, Screen.height / 2 + 100,
        Screen.width - 10, 200));
        // Load the main scene
        // The scene needs to be added into build setting to be loaded!
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("New Game", GUILayout.Width(400), GUILayout.Height(60)))
        {
            Application.LoadLevel("levelScene");
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        /*        if (GUILayout.Button("High score"))
                {
                    Debug.Log("You should implement a high score screen.");
                }*/
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Exit", GUILayout.Width(400), GUILayout.Height(60)))
        {
            Application.Quit();
            Debug.Log("Application.Quit() only works in build,not in editor");
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
