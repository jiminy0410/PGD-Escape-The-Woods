using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashBar : MonoBehaviour
{

    public Slider slider;

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
