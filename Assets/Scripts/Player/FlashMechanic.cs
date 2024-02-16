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

    [SerializeField]
    private AudioSource flashSound, flashRechargeSFX, flashFullSFX;
    public GameObject onLightning;
    public GameObject offLightning;
    private AnalyticsSystem anal;

    private GameObject player;

    public FlashBar flashBar;
    private bool flashligthFull;
    public bool Q;
    private bool canFlash = true;

    public static Difficulty selectedDifficulty;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    void Start()
    {

        ChargeMax = 0.5f;
        standingChargeRate = ChargeMax;

        flashCharges = maxFlashCharges;
        //playerVision = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        flashBar.SetMaxCharges(maxFlashCharges);
        //anal = GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>();

        player = GameObject.Find("Player");

        switch (selectedDifficulty)
        {
            case Difficulty.Easy:
                WiggleChargeMax = 0.7f;
                chargeRateRecovery = 0.0005f;
                lightDecay = 7f;
                defaultOuterRadius = 0f;
                enlargedOuterRadius = 20;
                standingChargeMin = 0.08f;
                standingChargeReduction = 0f;
                break;
            case Difficulty.Medium:
                WiggleChargeMax = 0.5f;
                chargeRateRecovery = 0.0005f;
                lightDecay = 10f;
                defaultOuterRadius = 0f;
                enlargedOuterRadius = 15;
                standingChargeMin = 0.08f;
                standingChargeReduction = 0.08f;
                break;
            case Difficulty.Hard:
                WiggleChargeMax = 0.5f;
                chargeRateRecovery = 0.001f;
                lightDecay = 13f;
                defaultOuterRadius = 0f;
                enlargedOuterRadius = 15f;
                standingChargeMin = 0.08f;
                standingChargeReduction = 0.12f;
                break;
        }

    }

    public void ButtonQ()
    {
        Q = !Q;
    }

    void Update()
    {
        //Debug.Log(selectedDifficulty);

        if (Input.GetButtonDown("Fire1") || Q)
        {
            //Debug.Log("click");
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //RaycastHit2D needs to be a list. Otherwise it will only return the first object it hits.
            RaycastHit2D[] hits = Physics2D.RaycastAll(raycastPos, Vector2.zero);
            // Check if the object hit has the tag "UI"

            canFlash = true;

            //This is to go through all of the hits returned by the RaycastHit2D and to check if it contains any colliders that have the UI tag.

            foreach (var hit in hits)
            {
                // Check if the object hit has the tag "UI"
                if (hit.collider.CompareTag("UI"))
                {
                    // If the object is UI, do not perform the action
                    //Debug.Log("Hit UI element. Ignoring action.");
                    canFlash = false;
                }
            }


            if (canFlash || Q)
            {
                if (flashCharges == maxFlashCharges)
                {
                    if (Q)
                        StartCoroutine(Flash((player.transform.position)));
                    else if (Input.GetButtonDown("Fire1"))
                        StartCoroutine(Flash(Camera.main.ScreenToWorldPoint(Input.mousePosition)));

                    if (gameObject.GetComponent<CharacterController2D>().Grounded == true)
                    {
                        //anal.flashesUsed++;
                        //anal.SendFlashEvent(false);
                    }
                    else if (gameObject.GetComponent<CharacterController2D>().Grounded == false)
                    {
                        //anal.flashesUsed++;
                        //anal.SendFlashEvent(true);
                    }

                    flashCharges--;
                    if (standingChargeRate > standingChargeMin) //when flashing, your next recharge goes slower when standing still
                    {
                        standingChargeRate -= standingChargeReduction;
                    }
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
            flashRechargeSFX.volume = 0.1f;
            gameObject.GetComponent<CharacterController2D>().playerAnim.SetBool("Shake", true);
        }
        else
        {
            offLightning.SetActive(true);
            currentFlashRechargeRate = standingChargeRate;
            onLightning.SetActive(false);
            flashRechargeSFX.volume = 0;
            gameObject.GetComponent<CharacterController2D>().playerAnim.SetBool("Shake", false);

        }


        if (flashCharges < maxFlashCharges)
        {
            flashCharges += currentFlashRechargeRate * Time.deltaTime;

        }

        if (playerVision.pointLightOuterRadius > defaultOuterRadius)
            playerVision.pointLightOuterRadius -= lightDecay * Time.deltaTime;

        if (flashCharges > 1)
        {
            flashFullSFX.pitch = Random.Range(0.8f, 1.0f);
            flashFullSFX.Play();
            flashCharges = 1;
        }

        if (playerVision.pointLightOuterRadius < 2)
        {
            playerVision.pointLightOuterRadius = 2;
            playerVision.intensity = 0;
        }

        flashBar.SetCharges(flashCharges);
    }

    public IEnumerator Flash(Vector3 targetLocation)
    {
        playerVision.gameObject.transform.position = new Vector3(targetLocation.x, targetLocation.y, 0);
        flashSound.pitch = Random.Range(2.5f, 3);
        flashSound.Play();
        playerVision.intensity = 1;
        playerVision.pointLightOuterRadius = enlargedOuterRadius;
        yield return null;
    }
}