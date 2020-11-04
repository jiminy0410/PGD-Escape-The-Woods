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
    public float flashRechargeRate = 0.2f;
    public static float lightDecay = 3f;
    public static float defaultOuterRadius = 2;

    private static float enlargedOuterRadius = 10;

    private AudioSource flashSound;

    public FlashBar flashBar;


    void Start()
    {
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
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            flashRechargeRate = 0.25f;

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            flashRechargeRate = 0.20f;

        if (flashCharges < maxFlashCharges)
            flashCharges += flashRechargeRate * Time.deltaTime;

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
        //flashSound.Play();
        playerVision.pointLightOuterRadius = enlargedOuterRadius;
        yield return null;
    }
}