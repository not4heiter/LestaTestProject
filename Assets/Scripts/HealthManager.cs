using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth = 0;
    public float deathYCoordinate = -10f;

    public GameObject[] hearts;
    public GameObject playerModel;
    public Animator playerAnimator;
    public GameObject deathScreen;

    public bool PauseGame;
    public bool isDead = false;

    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerModel.GetComponent<Transform>().position.y < deathYCoordinate)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                Destroy(hearts[i].gameObject);
            }

        }
        else
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(i < currentHealth);
            }
        }
    
        if (currentHealth <= 0 && !isDead)
        {
            if (!PauseGame)
            {
                isDead = true;
                StartCoroutine(HandleDeath());
            }
        }

        if (playerModel.GetComponent<Transform>().position.y < deathYCoordinate)
        {
            if (!PauseGame)
            {
                Pause();
            }
        }

    }

    private IEnumerator HandleDeath()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            playerAnimator.SetBool("isDead", isDead);
            playerObj.GetComponent<PlayerController>().canMove = false;
            yield return new WaitForSeconds(1f);

            Pause();
            playerObj.GetComponent<PlayerController>().canMove = true;
        }
    }


    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            audioSource.PlayOneShot(hitSound);
        }
        else if (currentHealth <= 0)
        {
            audioSource.PlayOneShot(deathSound);
            currentHealth = 0;
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        deathScreen.SetActive(true);
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
