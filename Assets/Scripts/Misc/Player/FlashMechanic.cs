using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashMechanic : MonoBehaviour
{
    public Light2D playerVision;
    public float maxFlashCharges = 3;
    public float flashCharges;
    public static float lightDecay = 3f;
    public static float defaultOuterRadius = 2;

    private static float enlargedOuterRadius = 10;

    private AudioSource flashSound;


    void Start()
    {
        flashCharges = maxFlashCharges;
        playerVision = GetComponent<Light2D>();
        flashSound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (flashCharges >= 1)
            {
                StartCoroutine(Flash());
                flashCharges--;
            }
        }

        //Debug.Log(playerVision.pointLightOuterRadius);
        
        if (playerVision.pointLightOuterRadius > defaultOuterRadius)
        {
            playerVision.pointLightOuterRadius -= lightDecay * Time.deltaTime;
            //Debug.Log(lightDecay);
        }
        

    }

    public IEnumerator Flash()
    {
        //flashSound.Play();
        playerVision.pointLightOuterRadius = enlargedOuterRadius;
        yield return null;
    }
}