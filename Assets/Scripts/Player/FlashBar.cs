using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashBar : MonoBehaviour
{

    public Slider slider;
    public Image phill;

    public FlashMechanic theFlash;

    public Color emptyColor, halfColor, fullColor;

    private void Start()
    {
        slider = GameObject.Find("FlashBar").GetComponent<Slider>();
        phill = GameObject.Find("FlashBar").transform.Find("Fill").GetComponent<Image>();
    }

    private void Update()
    {
        //the greater the charge speed, the 'better' the color. changing from red (slow) to green (fast)
        if (theFlash.standingChargeRate > theFlash.ChargeMax/2)
        {
            //If the player has more than half of their chargespeed left, the bar will change color between green and yellow.
            phill.color = Color.Lerp(halfColor, fullColor, (theFlash.standingChargeRate - theFlash.ChargeMax / 2) / (theFlash.ChargeMax - theFlash.ChargeMax / 2));
        }
        else
        {
            //Otherwise, they must have less than half of their charge speed left. so,the bar color will change betweeen yellow and red.
            phill.color = Color.Lerp(emptyColor, halfColor, (theFlash.standingChargeRate - theFlash.standingChargeMin) / ((theFlash.ChargeMax/2) - theFlash.standingChargeMin));
        }
        
        phill.color = new Color(phill.color.r, phill.color.g, phill.color.b, 1);
    }

    public void SetMaxCharges(float charges)
    {
        slider.maxValue = charges;
        slider.value = charges;
    }

    public void SetCharges(float charges)
    {
        slider.value = charges;
    }
}
