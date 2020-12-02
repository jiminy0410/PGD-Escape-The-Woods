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
            tileAlpha = 0.0255f * playerCollisionOpacity;
        }

        tileRenderer.color = new Color(tileRenderer.color.r, tileRenderer.color.g, tileRenderer.color.b, tileAlpha);

    }

    public void MakeTransparent()
    {
        tileAlpha = 0f;
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision == GameObject.Find("Player").GetComponent<BoxCollider2D>())
        {
            collidingWithPlayer = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collidingWithPlayer = false;
        }
    }
}
