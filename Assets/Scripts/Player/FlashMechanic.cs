using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashMechanic : MonoBehaviour
{
    public Light2D playerVision;
    public float maxFlashCharges = 1;
    public float flashCharges;
    public float currentFlashRechargeRate;
    public static float lightDecay;
    public static float defaultOuterRadius;
    public float standingChargeMin;
    public float ChargeMax;
    public float standingChargeReduction;
    public float standingChargeRate;

    private static float enlargedOuterRadius;

    private AudioSource flashSound;
    public GameObject onLightning;
    public GameObject offLightning;

    public FlashBar flashBar;

    public static Difficulty selectedDifficulty;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    void Start()
    {
        flashSound = this.GetComponent<AudioSource>();

        standingChargeRate = ChargeMax;

        flashCharges = maxFlashCharges;
        playerVision = GetComponent<Light2D>();
        flashSound = this.GetComponent<AudioSource>();
        flashBar.SetMaxCharges(maxFlashCharges);

        switch(selectedDifficulty)
        {
            case Difficulty.Easy:
                ChargeMax = 0.75f;
                currentFlashRechargeRate = 0.25f;
                lightDecay = 10f;
                defaultOuterRadius = 3f;
                enlargedOuterRadius = 18;
                standingChargeMin = 0.008f;
                standingChargeReduction = 0.008f;
                break;
            case Difficulty.Medium:
                ChargeMax = 0.5f;
                currentFlashRechargeRate = 0.2f;
                lightDecay = 13f;
                defaultOuterRadius = 2f;
                enlargedOuterRadius = 15;
                standingChargeMin = 0.008f;
                standingChargeReduction = 0.008f;
                break;
            case Difficulty.Hard:
                ChargeMax = 0.25f;
                currentFlashRechargeRate = 0.15f;
                lightDecay = 16f;
                defaultOuterRadius = 1.5f;
                enlargedOuterRadius = 12.5f;
                standingChargeMin = 0.008f;
                standingChargeReduction = 0.008f;
                break;
        }
    }

    void Update()
    {
        Debug.Log(selectedDifficulty);

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

        if (standingChargeRate < standingChargeMin) //make sure the recharge doesn't go TOO slow...
        {
            standingChargeRate = standingChargeMin;
        }

        if ((flashCharges == maxFlashCharges) && (standingChargeRate < ChargeMax)) //if you wait, you get your charge speed back!
        {
            standingChargeRate += 0.0005f; //TODO: make this a variable and tweak it
        }


        if (gameObject.GetComponent<CharacterController2D>().wiggleWiggleWiggle == 0)
        {
            offLightning.SetActive(false);
            currentFlashRechargeRate = ChargeMax;
            onLightning.SetActive(true);
        }
        else
        {
            offLightning.SetActive(true);
            currentFlashRechargeRate = standingChargeRate;
            onLightning.SetActive(false);
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