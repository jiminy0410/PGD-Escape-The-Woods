using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystems : DoorComponents
{
    void Start()
    {
        player = GameObject.Find("Player");
        cooldownCount = Time.time + cooldownTime;

        levelManager = GameObject.Find("LevelManager");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //if (GetComponent<FlashMechanic>().flashCharges <= GetComponent<FlashMechanic>().maxFlashCharges)
            //{
            //    GetComponent<FlashMechanic>().flashCharges = GetComponent<FlashMechanic>().maxFlashCharges;
            //}

            if (Input.GetAxisRaw("Vertical") > 0 && Time.time > cooldownCount)
            {
                Debug.Log("PLAYER IS PRESSING W");

                if (!finalDoor)
                {
                    if (nextDoor != null)
                    {
                        player.transform.position = nextDoor.transform.position;
                        cooldownCount = Time.time + cooldownTime;
                        nextDoor.gameObject.GetComponent<DoorSystems>().cooldownCount = this.cooldownCount;

                        GameObject deathPit = GameObject.Find("DeathPit");
                        deathPit.GetComponent<DeathPit>().respawnPoint = nextDoor;
                    }
                    else
                    {
                        levelManager.GetComponent<LevelManager>().LoadNextLevel();
                    }
                }
                else
                {
                    //NOT THE FINAL RESULT, CHANGE THIS TO THE VICTORY SCREEN OR SOMETHING
                    Application.Quit();
                }
            }
        }
    }
}
