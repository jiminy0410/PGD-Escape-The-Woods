using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{
    [HideInInspector]
    public GameObject player = null;

    public Transform respawnPoint;
    void Start()
    {
        player = GameObject.Find("player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.GetComponent<FlashMechanic>().StartCoroutine("Flash");
        player.GetComponent<FlashMechanic>().flashCharges = player.GetComponent<FlashMechanic>().maxFlashCharges;
        player.transform.position = respawnPoint.position;
    }
}
