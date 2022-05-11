using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class AnalyticsSystem : MonoBehaviour
{

    public int deathCount;
    public float timePlayed;


    void Start()
    {
        AnalyticsEvent.GameStart();
    }


    void Update()
    {
        
    }

    public void SendAnalytics()
    {
        timePlayed = Time.time;

        Dictionary<string, object> analyticsValues = new Dictionary<string, object>
        {
            {"TimePlayed", timePlayed},
            {"DeathCount", deathCount}
        };

        AnalyticsEvent.Custom("Testing Metrics", analyticsValues);
    }
}
