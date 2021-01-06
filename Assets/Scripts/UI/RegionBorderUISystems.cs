using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionBorderUISystems : RegionBorderUIComponents
{
    void Start()
    {
        revealing = true;
    }

    void Update()
    {
        regionColor.a = regionNameAlpha;
        GetComponent<Text>().color = regionColor;

        if (regionNameAlpha > 1)
        {
            regionNameAlpha = 1;

            if (revealing)
                revealing = false;
        }

        if (regionNameAlpha < 0)
        {
            regionNameAlpha = 0;
        }

        if (revealing)
        {

            regionNameAlpha = Mathf.Lerp(regionColor.a, revealTime, revealSpeed * Time.deltaTime);
        }
        else
        {
            regionNameAlpha = Mathf.Lerp(regionColor.a, -0.1f, revealSpeed * Time.deltaTime);
        }
    }

    public void StartReveal(string regionName_, Color regionColor_)
    {
        revealing = false;
        GetComponent<Text>().text = regionName_;
        regionColor = regionColor_;
        revealing = true;
    }
}
