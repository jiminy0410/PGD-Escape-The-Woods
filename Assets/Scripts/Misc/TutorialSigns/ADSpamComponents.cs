using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADSpamComponents : MonoBehaviour
{


    public GameObject[] letters;

    [HideInInspector]
    public int letterIndex;

    public Slider adSpamSlider;

    [HideInInspector]
    public CharacterController2D playerCC;
    public float chargeSpeed, decaySpeed;
    public float indicatorSpeed, indicatorTime;
}
