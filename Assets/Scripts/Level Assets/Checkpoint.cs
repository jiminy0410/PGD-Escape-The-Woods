using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject deathPit = GameObject.Find("DeathPit");
            deathPit.GetComponent<DeathPit>().respawnPoint = this.transform;

            if (FlashMechanic.flashCharges <= FlashMechanic.maxFlashCharges)
            {
                FlashMechanic.flashCharges = FlashMechanic.maxFlashCharges;
            }    
        }
    }
}
