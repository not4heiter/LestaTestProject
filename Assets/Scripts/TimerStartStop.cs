using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerStartStop : MonoBehaviour
{
    public enum BlockType { Start, Stop }
    public BlockType blockType;
    public TimerLogic timerLogic;
    private static bool hasPassedStartBlock = false;
    
    public GameObject winScreen;
    public Text timerWinText;
    public bool PauseGame;

    void Start()
    {
        hasPassedStartBlock = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (blockType == BlockType.Start && !hasPassedStartBlock)
            {
                timerLogic.StartTimer();
                hasPassedStartBlock = true;
            }
            else if (blockType == BlockType.Stop && hasPassedStartBlock)
            {                
                timerLogic.StopTimer();
                if (!PauseGame)
                {
                    DisplayFinalTime();
                    Pause();
                }
            }
        }
    }

    private void DisplayFinalTime()
    {
        float finalTime = timerLogic.ElapsedTime;
        int minutes = (int)(finalTime / 60);
        int seconds = (int)(finalTime % 60);
        float milliseconds = (finalTime - Mathf.Floor(finalTime)) * 100;


        timerWinText.text = string.Format("Your time: " + "{0:D2}:{1:D2}:{2:00}", minutes, seconds, (int)milliseconds);
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
        Camera.main.GetComponent<CameraController>().canRotate = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
}

