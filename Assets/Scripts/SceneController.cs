using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void GameplayScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
