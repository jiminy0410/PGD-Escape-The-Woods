using System.Collections;
using UnityEngine;


public class FlashMechanic : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D playerVision;
    public float maxFlashCharges = 1;
    public float flashCharges;
    public float currentFlashRechargeRate;
    public static float lightDecay;
    public static float defaultOuterRadius;
    public float standingChargeMin;
    public float ChargeMax, WiggleChargeMax;
    public float standingChargeReduction;
    public float standingChargeRate;
    public float chargeRateRecovery;

    private static float enlargedOuterRadius;

    private AudioSource flashSound;
    public GameObject onLightning;
    public GameObject offLightning;
    private AnalyticsSystem anal;

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

        ChargeMax = 0.5f;
        standingChargeRate = ChargeMax;

        flashCharges = maxFlashCharges;
        playerVision = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        flashSound = this.GetComponent<AudioSource>();
        flashBar.SetMaxCharges(maxFlashCharges);
        anal = GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>();


        switch (selectedDifficulty)
        {
            case Difficulty.Easy:
                WiggleChargeMax = 0.7f;
                chargeRateRecovery = 0.0005f;
                lightDecay = 10f;
                defaultOuterRadius = 3f;
                enlargedOuterRadius = 20;
                standingChargeMin = 0.08f;
                standingChargeReduction = 0f;
                break;
            case Difficulty.Medium:
                WiggleChargeMax = 0.5f;
                chargeRateRecovery = 0.0005f;
                lightDecay = 13f;
                defaultOuterRadius = 2f;
                enlargedOuterRadius = 15;
                standingChargeMin = 0.08f;
                standingChargeReduction = 0.08f;
                break;
            case Difficulty.Hard:
                WiggleChargeMax = 0.5f;
                chargeRateRecovery = 0.001f;
                lightDecay = 16f;
                defaultOuterRadius = 2f;
                enlargedOuterRadius = 15f;
                standingChargeMin = 0.08f;
                standingChargeReduction = 0.12f;
                break;
        }
    }

    void Update()
    {
        //Debug.Log(selectedDifficulty);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (flashCharges == maxFlashCharges)
            {
                StartCoroutine(Flash());
                if (gameObject.GetComponent<CharacterController2D>().Grounded == true)
                {
                    anal.flashesUsed++;
                    anal.SendFlashEvent(false);
                }
                else if (gameObject.GetComponent<CharacterController2D>().Grounded == false)
                {
                    anal.flashesUsed++;
                    anal.SendFlashEvent(true);
                }

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
            standingChargeRate += chargeRateRecovery; //TODO: make this a variable and tweak it
        }


        if (gameObject.GetComponent<CharacterController2D>().wiggleWiggleWiggle == 0)
        {
            offLightning.SetActive(false);
            currentFlashRechargeRate = WiggleChargeMax;
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