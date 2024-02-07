using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class FuelBasedLight : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D playerVision;

    public float maxFuel = 3;
    public float currentFuel;
    private float useRate = 0;

    public float lightDecay = 3f;
    public float lightGrowth = 3f;

    public float minimumOuterRadius = 2;
    public float enlargedOuterRadius = 10;

    private AudioSource flashSound;

    public FuelBar fuelBar;


    void Start()
    {
        currentFuel = maxFuel;
        playerVision = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        flashSound = this.GetComponent<AudioSource>();
        fuelBar.SetMaxFuel(maxFuel);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && (currentFuel >= 1) && (playerVision.pointLightOuterRadius < enlargedOuterRadius))
        {
            //StartCoroutine(Flash());
            playerVision.pointLightOuterRadius += lightGrowth * Time.deltaTime;
            Debug.Log("Growing");

        }
        else if ((Input.GetKey(KeyCode.E) || currentFuel <= 0) && (playerVision.pointLightOuterRadius > minimumOuterRadius) )
        {
            playerVision.pointLightOuterRadius -= lightDecay * Time.deltaTime;
            Debug.Log("Shrinking");
        }

        if (playerVision.pointLightOuterRadius <2)
        {
            playerVision.pointLightOuterRadius = 2;
        }

        fuelBar.SetFuel(currentFuel);

        useRate = (playerVision.pointLightOuterRadius - minimumOuterRadius)*0.1f;

        currentFuel -= useRate;

    }
    /*
    public IEnumerator Flash()
    {
        playerVision.pointLightOuterRadius = enlargedOuterRadius;
        //flashSound.Play();
        yield return null;
    }*/
}