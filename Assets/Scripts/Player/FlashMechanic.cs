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
    public float flashRechargeRate;
    public float flashRechargeStat = 0.08f;
    public float flashRechargeDyn = 0.5f;
    public static float lightDecay = 12f;
    public static float defaultOuterRadius = 2;

    private static float enlargedOuterRadius = 10;

    private AudioSource flashSound;

    public FlashBar flashBar;


    void Start()
    {
        flashSound = this.GetComponent<AudioSource>();

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

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            flashRechargeRate = flashRechargeDyn;

        }
        else
            flashRechargeRate = flashRechargeStat;

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
        flashSound.pitch = Random.Range(2.5f, 3);
        flashSound.Play();
        playerVision.pointLightOuterRadius = enlargedOuterRadius;
        yield return null;
    }
}