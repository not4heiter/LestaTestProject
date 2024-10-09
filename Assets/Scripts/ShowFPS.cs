using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    public float fps;
    public Text fpsText;

    private void Update()
    {
        if (Time.deltaTime > 0)
        {
            fps = 1.0f / Time.deltaTime;
        }
        else
        {
            fps = 0;
        }

        fpsText.text = "FPS: " + Mathf.Max(0, (int)fps);
    }
}
