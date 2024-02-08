using UnityEngine;

using System.Linq;

public class Checkpoint : MonoBehaviour
{
    public int id;
    public Transform resetPoint;

    public bool isTouched;

    [HideInInspector]
    public float cooldownCount;
    public float cooldownTime;
    public GameObject levelState;
    private AnalyticsSystem analytics;

    private void Start()
    {

        //cooldownCount = Time.time + cooldownTime;
        levelState = GameObject.Find("LevelResetter");
        //analytics = GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>();

        //analytics.levelStartTime = (int)Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == GameObject.Find("Player").GetComponent<CircleCollider2D>())
        {
            if (Time.time > cooldownCount)
            {
                if (!isTouched) //this part of the code is here to turn the corresponding lantern 'on'.
                {
                    foreach (Checkpoint checkpoint in GameObject.FindObjectsOfType<Checkpoint>())
                    {
                        if (checkpoint.isTouched)
                        {
                            //here, all checkpoints lanterns are turned off.
                            checkpoint.isTouched = false;
                            checkpoint.transform.Find("TX Village Props Road Lamp Light On").transform.Find("Point Light 2D").GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius = 1.5f;
                        }
                    }
                    //then, here, the light that's part of this scripts parent, will be turned 'on'
                    isTouched = true;
                    this.transform.Find("TX Village Props Road Lamp Light On").transform.Find("Point Light 2D").GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius = 4.5f;
                }

                levelState.GetComponent<levelState>().SavePointInTime();
                GameObject deathPit = GameObject.Find("DeathPit");
                deathPit.GetComponent<DeathPit>().respawnPoint = resetPoint;

                collision.GetComponent<FlashMechanic>().StartCoroutine("Flash");
                collision.GetComponent<FlashMechanic>().flashCharges = collision.GetComponent<FlashMechanic>().maxFlashCharges;

                collision.GetComponent<FlashMechanic>().standingChargeRate = collision.GetComponent<FlashMechanic>().ChargeMax;

                SaveSystem.SavePosition(this);
                SaveSystem.SaveScene();
                cooldownCount = Time.time + cooldownTime;

                //analytics.timePlayedThisLevel += (int)Time.time - analytics.levelStartTime;
                //analytics.levelStartTime = (int)Time.time;

                //this is here to keep track of the amount of checkpoint touches. for the analytics system
                //analytics.checkpointTouch++;
                //SaveSystem.SaveTestData(analytics.GetComponent<AnalyticsSystem>());
                //analytics.SendCheckpointEvent(id);

            }
        }
    }
}
