using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentScene;

    private void Awake() 
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    
    public void RestartToLevelOne()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
