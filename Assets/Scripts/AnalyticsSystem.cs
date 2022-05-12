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
            Debug.Log("send shit");
        }
    }

    public void SendAnalytics()
    {

        timePlayed = Time.time;

        Dictionary<string, object> analyticsValues = new Dictionary<string, object>
        {
            {"timeElapsed", timePlayed},
            {"deathCount", deathCount},
            {"flashGround", flashGround},
            {"flashAir", flashAir}
        };

        AnalyticsService.Instance.CustomData("analyticsValues", analyticsValues);
        AnalyticsService.Instance.Flush();
    }
}
