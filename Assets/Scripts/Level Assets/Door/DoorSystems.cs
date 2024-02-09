using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystems : DoorComponents
{

    //public AnalyticsSystem analSys;
    [SerializeField] private AudioSource teleportSFX;

    void Start()
    {
        player = GameObject.Find("Player");
        cooldownCount = Time.time + cooldownTime;

        levelManager = GameObject.Find("LevelManager");

        //analSys = GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>();
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

                        teleportSFX.pitch = Random.RandomRange(0.7f, 1.0f);
                        teleportSFX.Play();

                        GameObject deathPit = GameObject.Find("DeathPit");
                    }
                    else
                    {
                        //analSys.timePlayedThisLevel += (int)Time.time - analSys.levelStartTime;

                        //load the next scene.
                        //GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>().SendLevelCompletionEvent();

                        //analSys.resetVariables();

                        SaveSystem.EraseData();

                        levelManager.GetComponent<LevelManager>().LoadNextLevel();
                    }
                }
                else
                {

                    //analSys.timePlayedThisLevel += (int) Time.time - analSys.levelStartTime;

                    //NOT THE FINAL RESULT, CHANGE THIS TO THE VICTORY SCREEN OR SOMETHING
                    //analSys.SendLevelCompletionEvent();

                    //analSys.resetVariables();

                    SaveSystem.EraseData();
                    Application.Quit();
                }
            }
        }
    }
}
