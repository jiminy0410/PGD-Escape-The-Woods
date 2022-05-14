using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [HideInInspector]
    public AnalyticsSystem analytic;
    private void Start()
    {
        if (SaveSystem.LoadTestData() != null)
        {
            analytic = GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>();
            analytic.deathCount = SaveSystem.LoadTestData().deathCount;
            analytic.timePlayedThisLevel = (int) SaveSystem.LoadTestData().timePlayedThisLevel;
            analytic.flashesUsed = SaveSystem.LoadTestData().flashesUsed;
            analytic.checkpointTouch = SaveSystem.LoadTestData().checkpointTouch;
        }
        else
        {
            Debug.LogWarning("No Test data found! it's probably fine, but if you were excpecting some...");
        }
        

    }

    public void PlayGame()
    {
        loadScene();
    }

    public void NewGame()
    {

        if (SaveSystem.LoadTestData() != null)
        {
            analytic.resetVariables();
            Debug.Log("RESETTING VARIABLES");
        }


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
