using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSpamSystems : ADSpamComponents
{

    void Start()
    {
        playerCC = GameObject.Find("Player").GetComponent<CharacterController2D>();

        indicatorTime = Time.time;
        letterIndex = 0;
    }


    void Update()
    {
        if (letterIndex < 0)
        {
            letterIndex = 1;
        }

        if (playerCC.wiggleWiggleWiggle == 0)
        {
            adSpamSlider.value += chargeSpeed;
        }
        else
        {
            adSpamSlider.value -= decaySpeed;
        }

        if (Time.time > indicatorTime + indicatorSpeed)
        {
            letters[letterIndex].SetActive(false);

            if (letterIndex - 1 < 0)
            {
                letters[letterIndex + 1].SetActive(true);
                letterIndex = 1;
            }
            else
            {
                letters[letterIndex - 1].SetActive(true);
                letterIndex--;
            }

            indicatorTime = Time.time;
        }


    }
}
