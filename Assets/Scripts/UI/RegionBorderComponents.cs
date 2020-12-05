using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionBorderComponents : MonoBehaviour
{
    [HideInInspector]
    public GameObject regionIndicatorUI;
    [HideInInspector]
    public float regionIndicatorUIAlpha;

    public string regionIndicatorText;
    public Color regionColor;

    [Space]

    public float revealSpeed;
    public int secondsNameRevealed;
    [HideInInspector]
    public bool revealing;
}
