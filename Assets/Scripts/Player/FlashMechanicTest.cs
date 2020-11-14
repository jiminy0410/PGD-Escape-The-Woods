using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashMechanicTest : MonoBehaviour
{
    public Light2D playerVision;
    public float maxFlashCharges = 1;
    public float flashCharges;
    public float flashRechargeRate = 0.2f;
    public static float lightDecay = 12f;
    public static float defaultOuterRadius = 2;

    public static float rechargeTapTime;
    public bool isRecharging;
    public KeyCode lastKeyCode;

    private static float enlargedOuterRadius = 10;

    private AudioSource flashSound;

    public FlashBar flashBar;

    CharacterController2D controller;


    void Start()
    {
        flashSound = this.GetComponent<AudioSource>();

        flashCharges = maxFlashCharges;
        playerVision = GetComponent<Light2D>();
        flashSound = this.GetComponent<AudioSource>();
        flashBar.SetMaxCharges(maxFlashCharges);

        controller = GetComponent<CharacterController2D>();
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            if ((rechargeTapTime > Time.time && lastKeyCode == KeyCode.A) && controller.Grounded == true && isRecharging == false)
            {
                isRecharging = true;
                StartCoroutine(Recharge());
            }
            else
            {
                rechargeTapTime = Time.time + 0.15f;
            }
            lastKeyCode = KeyCode.D;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if ((rechargeTapTime > Time.time && lastKeyCode == KeyCode.D) && controller.Grounded == true && isRecharging == false)
            {
                isRecharging = true;
                StartCoroutine(Recharge());
            }
            else
            {
                rechargeTapTime = Time.time + 0.15f;
            }
            lastKeyCode = KeyCode.A;
        }

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

    IEnumerator Recharge()
    {
        flashRechargeRate = 0.8f;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(0.2f);

        flashRechargeRate = 0.2f;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        isRecharging = false;

        yield return null;
    }
}