using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [ReadOnly] public float fixedWidth;
    [ReadOnly] public int orthographicSize = 5;

    #region MonoBehaviour
    void Update()
    {
        if (GameMode.Instance)
        {
            Camera.main.orthographicSize = fixedWidth / (Camera.main.aspect);
        }
    }
    void Start()
    {
        if (GameMode.Instance)
        {
            fixedWidth = (float)orthographicSize * ((float)Screen.width / (float)Screen.height);
        }
    }

    #endregion
}
