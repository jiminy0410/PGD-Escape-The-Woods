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

            collision.GetComponent<FuelBasedLight>().currentFuel = collision.GetComponent<FuelBasedLight>().maxFuel;

            //if (GetComponent<FlashMechanic>().flashCharges <= GetComponent<FlashMechanic>().maxFlashCharges)
            //{
            //    GetComponent<FlashMechanic>().flashCharges = GetComponent<FlashMechanic>().maxFlashCharges;
            //}    
        }
    }
}
