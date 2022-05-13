using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class AnalyticsSystem : MonoBehaviour
{

    public int deathCount;
    public float timePlayed;
    public int flashGround;
    public int flashAir;
    public int checkpointTouch;


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
        if(Input.GetKeyDown(KeyCode.O))
        {
            SendAnalytics();
            Debug.Log("send shit");
        }
    }

    public void SendAnalytics()
    {

        timePlayed = Time.time;

        Debug.Log("doing the dictionary");

        Dictionary<string, object> analyticsValues = new Dictionary<string, object>
        {
            {"timeElapsed", timePlayed},
            {"deathCount", deathCount},
            {"flashGround", flashGround},
            {"flashAir", flashAir},
            {"checkpointTouched", checkpointTouch}

        };

        AnalyticsService.Instance.CustomData("analyticsValues", analyticsValues);
        AnalyticsService.Instance.Flush();
    }
}
