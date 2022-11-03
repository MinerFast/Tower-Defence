using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;

    public bool showInfor = true;

    public int setFPS = 60;

    public Vector2 resolution = new Vector2(1280, 720);

    #region MonoBehaviour
    void OnGUI()
    {
        if (showInfor)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);


            GUI.Label(rect, text, style);

            Rect rect2 = new Rect(250, 0, w, h * 2 / 100);
            GUI.Label(rect2, Screen.currentResolution.width + "x" + Screen.currentResolution.height, style);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Screen.SetResolution((int)resolution.x, (int)resolution.y, true);
        Application.targetFrameRate = setFPS;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    #endregion
}
