using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        loadScene();
    }

    public void NewGame()
    {
        SaveSystem.EraseData();

        string sceneName = SaveSystem.LoadScene();
        if (sceneName == null)
        {
            Debug.Log("no save data found. starting in level 1...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void loadScene()
    {
        string sceneName = SaveSystem.LoadScene();
        if (sceneName == null)
        {
            Debug.Log("no save data found. starting in level 1...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT IS WORKING");
        Application.Quit();
    }
}
