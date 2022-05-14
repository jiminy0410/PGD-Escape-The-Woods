using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnalyticsSystem : MonoBehaviour
{
    [HideInInspector]
    public int levelStartTime;

    public int deathCount;
    public int timePlayedThisLevel;
    public int flashesUsed;

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
            var options = new InitializationOptions();
            options.SetEnvironmentName("testing_escapethewoods");

            await UnityServices.InitializeAsync(options);
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log("Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately");
        }
    }


    void Update()
    {

        //Test function to check if data is being sent. Not inteded for persistant use.
        if (Input.GetKeyDown(KeyCode.O))
        {
            //Debug.Log("send shit");
            //SendAnalytics();
        }
    }

    //public void SendAnalytics()
    //{

    //    Dictionary<string, object> analyticsValues = new Dictionary<string, object>
    //    {
    //        {"timePassed", timePlayedThisLevel},
    //        {"deathCount", deathCount},
    //        {"flashGround", flashGround},
    //        {"flashAir", flashAir},
    //        {"checkpointTouched", checkpointTouch},
    //        {"levelCompleted", SceneManager.GetActiveScene().name}


    //    };

    //    AnalyticsService.Instance.CustomData("analyticsValues", analyticsValues);
    //    AnalyticsService.Instance.Flush();

    //    Debug.Log("Analytics have been sent!");
    //}

    public void SendFlashEvent(bool aerialFlash)
    {
        Dictionary<string, object> flashEvents = new Dictionary<string, object>
        {
            {"flashesUsed", flashesUsed},
            {"Flash_Aerial", aerialFlash},
            {"timePassed", timePlayedThisLevel},
            {"currentLevel", SceneManager.GetActiveScene().name}
        };

        AnalyticsService.Instance.CustomData("FlashEvents", flashEvents);
        AnalyticsService.Instance.Flush();
    }

    public void SendDeathEvent()
    {
        Dictionary<string, object> deathEvent = new Dictionary<string, object>
        {
            {"deathCount", deathCount},
            {"timePassed", timePlayedThisLevel},
            {"currentLevel", SceneManager.GetActiveScene().name}
        };

        AnalyticsService.Instance.CustomData("DeathEvent", deathEvent);
        AnalyticsService.Instance.Flush();
    }

    public void SendLevelCompletionEvent()
    {
        Dictionary<string, object> levelCompletionEvent = new Dictionary<string, object>
        {
            {"deathCount", deathCount},
            {"timePassed", timePlayedThisLevel},
            {"flashesUsed", flashesUsed},
            {"checkpointTouched", checkpointTouch},
            {"currentLevel", SceneManager.GetActiveScene().name}
        };

        AnalyticsService.Instance.CustomData("LevelCompletionEvent", levelCompletionEvent);
        AnalyticsService.Instance.Flush();
    }

    public void SendCheckpointEvent(int checkpointID)
    {
        Dictionary<string, object> checkpointEvent = new Dictionary<string, object>
        {
            {"checkpointTouched", checkpointTouch},
            {"checkpointID", checkpointID},
            {"timePassed", timePlayedThisLevel},
            {"currentLevel", SceneManager.GetActiveScene().name}
        };

        AnalyticsService.Instance.CustomData("CheckpointEvent", checkpointEvent);
        AnalyticsService.Instance.Flush();
    }

    public void resetVariables()
    {
        deathCount = flashesUsed = checkpointTouch = 0;
        timePlayedThisLevel = 0;
        levelStartTime = 0;
    }
}
