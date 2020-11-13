using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashMechanic : MonoBehaviour
{
    public Light2D playerVision;
    public float maxFlashCharges = 1;
    public float flashCharges;
    public float currentFlashRechargeRate = 0.2f;
    public static float lightDecay = 12f;
    public static float defaultOuterRadius = 2;
    public float standingChargeMin = 0.08f;
    public float ChargeMax = 0.5f;
    public float standingChargeReduction = 0.08f;
    public float standingChargeRate;

    private static float enlargedOuterRadius = 10;

    private AudioSource flashSound;

    public FlashBar flashBar;


    void Start()
    {
        flashSound = this.GetComponent<AudioSource>();

        standingChargeRate = ChargeMax;

        flashCharges = maxFlashCharges;
        playerVision = GetComponent<Light2D>();
        flashSound = this.GetComponent<AudioSource>();
        flashBar.SetMaxCharges(maxFlashCharges);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (flashCharges == maxFlashCharges)
            {
                StartCoroutine(Flash());
                flashCharges--;
                if (standingChargeRate > standingChargeMin) //when flashing, your next recharge goes slower when standing still
                {
                    standingChargeRate -= standingChargeReduction;
                }
            }
        }

        if(standingChargeRate < standingChargeMin) //make sure the recharge doesn't go TOO slow...
        {
            standingChargeRate = standingChargeMin;
        }

        if ((flashCharges == maxFlashCharges) && (standingChargeRate < ChargeMax)) //if you wait, you get your charge speed back!
        {
            standingChargeRate += 0.0005f; //TODO: make this a variable and tweak it
        }


        if (Input.GetAxisRaw("Horizontal") != 0)
            currentFlashRechargeRate = ChargeMax;
        else
        {
            currentFlashRechargeRate = standingChargeRate;
        }


        if (flashCharges < maxFlashCharges)
            flashCharges += currentFlashRechargeRate * Time.deltaTime;

        if (playerVision.pointLightOuterRadius > defaultOuterRadius)
            playerVision.pointLightOuterRadius -= lightDecay * Time.deltaTime;

        if (flashCharges > 1)
            flashCharges = 1;

        if (playerVision.pointLightOuterRadius < 2)
            playerVision.pointLightOuterRadius = 2;

        flashBar.SetCharges(flashCharges);
    }

    public IEnumerator Flash()
    {
        flashSound.pitch = Random.Range(2.5f, 3);
        flashSound.Play();
        playerVision.pointLightOuterRadius = enlargedOuterRadius;
        yield return null;
    }
}