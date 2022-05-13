using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnalyticsSystem : MonoBehaviour
{

    public int deathCount;
    public float timePlayedThisLevel;
    public int flashGround;
    public int flashAir;
    public int checkpointTouch;

    private static AnalyticsSystem instance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    async void Start()
    {

       


        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log("Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately");
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            //this is kinda sad :(
            //dit is om aan te geven dat het level is gehaald. niet doen tenzij het level klaar is. anders wordt de data nogal niet goed.
            //AnalyticsEvent.LevelComplete(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().buildIndex);
            //haha, nee. het doet precies niets. moeten we nog ff naar kijken.
            SendAnalytics();
            Debug.Log("send shit");
        }
    }

    public void SendAnalytics()
    {


        Debug.Log("doing the dictionary");

        Dictionary<string, object> analyticsValues = new Dictionary<string, object>
        {
            {"timeElapsed", timePlayedThisLevel},
            {"deathCount", deathCount},
            {"flashGround", flashGround},
            {"flashAir", flashAir},
            {"checkpointTouched", checkpointTouch},
            {"levelCompleted", SceneManager.GetActiveScene().name}


        };

        AnalyticsService.Instance.CustomData("analyticsValues", analyticsValues);
        AnalyticsService.Instance.Flush();

        Debug.Log("Analytics have been sent!");
    }
}
