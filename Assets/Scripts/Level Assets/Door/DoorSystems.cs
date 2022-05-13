using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystems : DoorComponents
{
    public float levelStartTime;

    void Start()
    {
        player = GameObject.Find("Player");
        cooldownCount = Time.time + cooldownTime;

        levelManager = GameObject.Find("LevelManager");

        levelStartTime = Time.time;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetAxisRaw("Vertical") < 0 && Time.time > cooldownCount)
            {
                Debug.Log("PLAYER IS PRESSING W");

                if (!finalDoor)
                {
                    if (nextDoor != null)
                    {
                        player.transform.position = nextDoor.transform.Find("TransportPoint").position;
                        cooldownCount = Time.time + cooldownTime;
                        nextDoor.gameObject.GetComponent<DoorSystems>().cooldownCount = this.cooldownCount;

                        GameObject deathPit = GameObject.Find("DeathPit");
                    }
                    else
                    {
                        GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>().timePlayedThisLevel = Time.time - levelStartTime;

                        //load the next scene.
                        SaveSystem.EraseData();
                        GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>().SendAnalytics();
                        levelManager.GetComponent<LevelManager>().LoadNextLevel();
                    }
                }
                else
                {
                    GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>().timePlayedThisLevel = Time.time - levelStartTime;

                    //NOT THE FINAL RESULT, CHANGE THIS TO THE VICTORY SCREEN OR SOMETHING
                    SaveSystem.EraseData();
                    GameObject.Find("AnalyticObject").GetComponent<AnalyticsSystem>().SendAnalytics();
                    Application.Quit();
                }
            }
        }
    }
}
