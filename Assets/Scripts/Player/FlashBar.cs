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

    private void Update()
    {
        if (theFlash.currentFlashRechargeRate > theFlash.ChargeMax/2)
        {
            phill.color = Color.Lerp(halfColor, fullColor, (theFlash.currentFlashRechargeRate- theFlash.ChargeMax / 2) / (theFlash.ChargeMax - theFlash.ChargeMax / 2));
        }
        else
        {
            phill.color = Color.Lerp(emptyColor, halfColor, theFlash.currentFlashRechargeRate / (theFlash.ChargeMax/2));
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
