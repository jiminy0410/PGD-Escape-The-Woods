using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionBorderSystems : RegionBorderComponents
{
    void Start()
    {
        regionIndicatorUIAlpha = 0f;
        regionIndicatorUI = GameObject.Find("RegionIndicatorUI");
    }

    void Update()
    {
        regionColor.a = regionIndicatorUIAlpha;
        regionIndicatorUI.GetComponent<Text>().color = regionColor;

        if (regionIndicatorUIAlpha > 1)
        {
            regionIndicatorUIAlpha = 1;
        }

        if (regionIndicatorUIAlpha < 0)
        {
            regionIndicatorUIAlpha = 0;
        }

        StartCoroutine(RevealRegionName(secondsNameRevealed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == GameObject.Find("Player").GetComponent<CircleCollider2D>())
        {
            regionIndicatorUI.GetComponent<Text>().text = regionIndicatorText;
            revealing = true;
        }
    }

    public IEnumerator RevealRegionName(int holdReveal)
    {
        if (!revealing)
        {
            regionIndicatorUIAlpha -= revealSpeed;
        }
        else
        {
            regionIndicatorUIAlpha += revealSpeed;
        }


        if (revealing && regionIndicatorUIAlpha >= 1)
        {
            yield return new WaitForSeconds(holdReveal);
            revealing = false;
        }
    }
}
