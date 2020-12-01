using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;

public class Checkpoint : MonoBehaviour
{
    public Transform resetPoint;

    public bool isTouched;

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
                if (!isTouched)
                {
                    foreach (Checkpoint checkpoint in GameObject.FindObjectsOfType<Checkpoint>())
                    {
                        if (checkpoint.isTouched)
                        {
                            checkpoint.isTouched = false;
                            checkpoint.transform.Find("TX Village Props Road Lamp Light On").transform.Find("Point Light 2D").GetComponent<Light2D>().pointLightOuterRadius = 1.5f;
                        }
                    }
                    
                    isTouched = true;
                    this.transform.Find("TX Village Props Road Lamp Light On").transform.Find("Point Light 2D").GetComponent<Light2D>().pointLightOuterRadius = 4.5f;
                }

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
