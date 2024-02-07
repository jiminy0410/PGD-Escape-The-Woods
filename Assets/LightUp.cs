using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightUp : MonoBehaviour
{

    public bool isTouched, glowGrow;

    public float glowing = 0.09f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTouched)
        {
            //this code is run the first time the player touches this. then, the update part should be able to run.
            isTouched = true;
            glowGrow = true;
        }
    }

    private void Update()
    {
        if (glowGrow)
        {
            //the plants glow starts to grow.
            if (!transform.Find("dubbleJumpGrass").transform.Find("GrassLight").GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled)
            {
                transform.Find("dubbleJumpGrass").transform.Find("GrassLight").GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
            }

            if (!transform.Find("Plant Particles").gameObject.activeSelf)
            {
                transform.Find("Plant Particles").gameObject.SetActive(true);
                Debug.Log("These plants particles weren't active. They should be now.");
            }
            

            foreach (UnityEngine.Rendering.Universal.Light2D light in transform.Find("dubbleJumpGrass").transform.GetComponentsInChildren<UnityEngine.Rendering.Universal.Light2D>())
            {
                if ((light.lightType == UnityEngine.Rendering.Universal.Light2D.LightType.Point) && (light.pointLightOuterRadius <= 0.09))
                {
                    light.pointLightOuterRadius += 0.0001f;
                }
            }
        }
    }
}
