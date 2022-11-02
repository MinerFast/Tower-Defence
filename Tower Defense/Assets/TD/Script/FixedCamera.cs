using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [ReadOnly] public float fixedWidth;
    [ReadOnly] public int orthographicSize = 5;
    // Start is called before the first frame update
    void Start()
    {
        if (GameMode.Instance)
        {
            //fixedWidth = orthographicSize * (GameMode.Instance.resolution.x / GameMode.Instance.resolution.y);
            fixedWidth = (float)orthographicSize * ((float)Screen.width / (float)Screen.height);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMode.Instance)
        {
            Camera.main.orthographicSize = fixedWidth / (Camera.main.aspect);
        }
    }
}
