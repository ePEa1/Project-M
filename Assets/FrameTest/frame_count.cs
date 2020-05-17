using UnityEngine;
using System.Collections;

public class frame_count : MonoBehaviour
{

    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 3 / 100; //font size
        style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 0.5f); //font color
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        if (fps < 60f)
        {
            style.normal.textColor = new Color(1.0f, 0.0f, 0.0f, 1.0f); //warning color
        }

        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}


