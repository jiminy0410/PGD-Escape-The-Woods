using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [HideInInspector]
    public AnalyticsSystem analytic;
    [SerializeField]
    private AudioSource clickSound;

    private PersistingSounds persistingSounds;

    [SerializeField]
    private Dropdown difficultyDropdown;

    private void Start()
    {
        persistingSounds = GameObject.Find("PersistingSounds").GetComponent<PersistingSounds>();
        clickSound = this.GetComponent<AudioSource>();
        if (SaveSystem.LoadTestData() != null)
        {
            //analytic = GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>();
            //analytic.deathCount = SaveSystem.LoadTestData().deathCount;
            //analytic.timePlayedThisLevel = (int) SaveSystem.LoadTestData().timePlayedThisLevel;
            //analytic.flashesUsed = SaveSystem.LoadTestData().flashesUsed;
            //analytic.checkpointTouch = SaveSystem.LoadTestData().checkpointTouch;
        }
        else
        {
            Debug.LogWarning("No Test data found! it's probably fine, but if you were excpecting some...");
        }

    }

    public void PlayGame()
    {
        persistingSounds.PlaySound("background");
        persistingSounds.PlaySound("ambient");
        loadScene();
    }

    public void NewGame()
    {
        StartCoroutine(klick());

        if (SaveSystem.LoadTestData() != null)
        {
            //analytic.resetVariables();
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

        persistingSounds.PlaySound("background");
        persistingSounds.PlaySound("ambient");

        SceneManager.LoadScene(sceneName);
    }

    public void loadScene()
    {
        StartCoroutine(klick());
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
        StartCoroutine(klick());
        Debug.Log("QUIT IS WORKING");
        Application.Quit();
    }

    public IEnumerator klick()
    {
        clickSound.pitch = Random.Range(1.5f, 2);
        clickSound.Play();
        yield return null;
    }
}
