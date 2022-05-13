using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{
    [HideInInspector]
    public GameObject player = null;
    public GameObject levelState;

    public Transform respawnPoint;

    private AnalyticsSystem analSys;
    void Start()
    {
        analSys = GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>();
        player = GameObject.Find("Player");
        levelState = GameObject.Find("LevelResetter");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Death();
    }

    public void Death()
    {
        analSys.deathCount++;

        player.GetComponent<FlashMechanic>().StartCoroutine("Flash");
        player.GetComponent<FlashMechanic>().flashCharges = player.GetComponent<FlashMechanic>().maxFlashCharges;
        player.GetComponent<FlashMechanic>().standingChargeRate = player.GetComponent<FlashMechanic>().ChargeMax;
        levelState.GetComponent<levelState>().Rvert();
        player.transform.position = respawnPoint.position;
    }
}
