using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerLogic : MonoBehaviour
{
    public Text timerText;
    private float elapsedTime;
    private bool isRunning;

    public float ElapsedTime => elapsedTime;

    void Start()
    {
        ResetTimer();        
    }

    public void StartTimer()
    {
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(TimerCoroutine());
        }
    }

    public void StopTimer()
    {
        if (isRunning)
        {
            isRunning = false;
            StopCoroutine(TimerCoroutine());
        }
    }

    public void ResetTimer()
    {
        timerText = GameObject.Find("StopWatch").GetComponent<Text>();
        elapsedTime = 0f;
        isRunning = false;
        UpdateTimerText();
    }

    private IEnumerator TimerCoroutine()
    {
        while (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }
    }

    public void UpdateTimerText()
    {
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        float milliseconds = (elapsedTime - Mathf.Floor(elapsedTime)) * 100;

        timerText.text = string.Format("{0:D2}:{1:D2}:{2:00}", minutes, seconds, (int)milliseconds);
    }
}