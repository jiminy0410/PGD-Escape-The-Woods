using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform resetPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject deathPit = GameObject.Find("DeathPit");
            deathPit.GetComponent<DeathPit>().respawnPoint = resetPoint;

            collision.GetComponent<FlashMechanic>().StartCoroutine("Flash");
            collision.GetComponent<FlashMechanic>().flashCharges = collision.GetComponent<FlashMechanic>().maxFlashCharges;

            //if (GetComponent<FlashMechanic>().flashCharges <= GetComponent<FlashMechanic>().maxFlashCharges)
            //{
            //    GetComponent<FlashMechanic>().flashCharges = GetComponent<FlashMechanic>().maxFlashCharges;
            //}    
        }
    }
}
