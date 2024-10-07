using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Update()
    {

    }

    public void GoToGame()
    {
        SceneTransition.SwitchToScene("GameScene"); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
