using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretWallSystems : SecretWallComponents
{

    void Start()
    {
        tileRenderer = GetComponent<Tilemap>();
        tileRenderer.color = new Color(tileRenderer.color.r, tileRenderer.color.g, tileRenderer.color.b, 255f);
    }


    void Update()
    {
        if (!collidingWithPlayer)
        {
            tileAlpha += 0.0255f * opacityRecovery;
        }
        else
        {
            tileAlpha -= 0.0255f * opacityRecovery;
        }

        if (tileAlpha < 0.0255f * playerCollisionOpacity)
            tileAlpha =  0.0255f * playerCollisionOpacity;
        else if(tileAlpha > 1)
        {
            tileAlpha = 1;
        }

        tileRenderer.color = new Color(tileRenderer.color.r, tileRenderer.color.g, tileRenderer.color.b, tileAlpha);

    }

    // && collision == GameObject.Find("Player").GetComponent<BoxCollider2D>()
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == GameObject.Find("Player").GetComponent<CircleCollider2D>())
        {
            collidingWithPlayer = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == GameObject.Find("Player").GetComponent<CircleCollider2D>())
        {
            collidingWithPlayer = false;
        }
    }
}
