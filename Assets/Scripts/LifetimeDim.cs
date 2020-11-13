using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifetimeDim : MonoBehaviour
{
    private float tint = 0;
    public float lifeTime;

    private bool biteable = false;
    
    public float blinks;
    public float colorIntensity;
    private float counter;
    public GameObject player;
    public Transform respawnPoint;

    public Color from, to;
    // Start is called before the first frame update
    void Start()
    {
        counter = blinks;
        colorIntensity = colorIntensity / 100;
        player = GameObject.Find("player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            biteable = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        tint += 0.02f*(colorIntensity)*(blinks/lifeTime);
        //image.r = tint;
        GetComponent<SpriteRenderer>().color = Color.Lerp(from, to, tint);
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1);
        if (tint >= 1*colorIntensity)
        {
            counter--;
            tint = 0;
        }
        if (counter == 0)
        {
            player.GetComponent<FuelBasedLight>().currentFuel = player.GetComponent<FuelBasedLight>().maxFuel;

            if (biteable)
            {
                player.transform.position = respawnPoint.position;
            }
            
            counter = blinks;
            GetComponent<SpriteRenderer>().color = Color.white;
            enabled = false;
        }
        biteable = false;

        //Debug.Log("ye boi");
    }
}
