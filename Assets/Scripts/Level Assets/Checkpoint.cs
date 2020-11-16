using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform resetPoint;

    [HideInInspector]
    public float cooldownCount;
    public float cooldownTime;
    public GameObject levelState;

    private void Start()
    {
        cooldownCount = Time.time + cooldownTime;
        levelState = GameObject.Find("LevelResetter");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time > cooldownCount)
            {

                levelState.GetComponent<levelState>().pointInTime = levelState.GetComponent<levelState>().currentPointInTime;
                GameObject deathPit = GameObject.Find("DeathPit");
                deathPit.GetComponent<DeathPit>().respawnPoint = resetPoint;

                collision.GetComponent<FlashMechanic>().StartCoroutine("Flash");
                collision.GetComponent<FlashMechanic>().flashCharges = collision.GetComponent<FlashMechanic>().maxFlashCharges;

                collision.GetComponent<FlashMechanic>().standingChargeRate = collision.GetComponent<FlashMechanic>().ChargeMax;

            cooldownCount = Time.time + cooldownTime;

            }
        }
    }
}
