using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionBorderSystems : RegionBorderComponents
{
    void Start()
    {
        regionIndicatorUI = GameObject.Find("RegionIndicatorUI");
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == GameObject.Find("Player").GetComponent<CircleCollider2D>())
        {
            regionIndicatorUI.GetComponent<RegionBorderUISystems>().StartReveal(regionIndicatorText, regionColor);
        }
    }
}
