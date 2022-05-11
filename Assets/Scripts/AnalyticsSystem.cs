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


    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately
        }
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SendAnalytics();
        }
    }

    public void SendAnalytics()
    {

        timePlayed = Time.time;

        Dictionary<string, object> analyticsValues = new Dictionary<string, object>
        {
            {"timeElapsed", timePlayed},
            {"deathCount", deathCount}
        };

        AnalyticsService.Instance.CustomData("analyticsValues", analyticsValues);
        AnalyticsService.Instance.Flush();
    }
}
