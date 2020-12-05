using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionBorderUIComponents : MonoBehaviour
{
    public int revealForSeconds;
    public float revealSpeed;
    public Color regionColor;

    [HideInInspector]
    public float regionNameAlpha;
    [HideInInspector]
    public bool revealing;
}
