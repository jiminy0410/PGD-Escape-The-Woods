using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{
    public GameObject Flash;
    public GameObject player = null;
    public Transform respawnPoint;
    void Start()
    {
        player = GameObject.Find("player");
        Flash = GameObject.Find("PlayerVision");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.transform.position = respawnPoint.position;
        //Flash.GetComponent<FlashMechanic>().StartCoroutine("Flash");
    }
}
