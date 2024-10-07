using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;
    public HealthManager healthManager;
    public TimerStartStop timerStartStop;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !healthManager.PauseGame && !timerStartStop.PauseGame)
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.GetComponent<CameraController>().canRotate = true;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseGameMenu.SetActive(true);
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
