using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FuelBasedLight : MonoBehaviour
{
    public Light2D playerVision;
    public float maxFuel = 1000;
    public float CurrentFuel;
    public float lightDecay = 3f;
    public float lightGrowth = 3f;
    public float defaultOuterRadius = 2;

    private static float enlargedOuterRadius = 10;

    private AudioSource flashSound;

    public FuelBar fuelBar;


    void Start()
    {
        CurrentFuel = maxFuel;
        fuelBar.SetMaxFuel(maxFuel);
        playerVision = GetComponent<Light2D>();
        flashSound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && (CurrentFuel >= 1) &&(playerVision.pointLightOuterRadius< enlargedOuterRadius))
        {
            //StartCoroutine(Flash());
            playerVision.pointLightOuterRadius += lightGrowth * Time.deltaTime;
            CurrentFuel--;
            fuelBar.SetFuel(CurrentFuel);
            Debug.Log("Growing");
        }
        else if (playerVision.pointLightOuterRadius > defaultOuterRadius)
        {
            playerVision.pointLightOuterRadius -= lightDecay * Time.deltaTime;
            Debug.Log("Shrinking");
        }

    }
    /*
    public IEnumerator Flash()
    {
        playerVision.pointLightOuterRadius = enlargedOuterRadius;
        //flashSound.Play();
        yield return null;
    }*/
}