using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretWallComponents : MonoBehaviour
{

    [HideInInspector]
    public float tileAlpha;

    public float opacityRecovery;
    public float playerCollisionOpacity;

    [HideInInspector]
    public Tilemap tileRenderer;

    [HideInInspector]
    public bool collidingWithPlayer;




}
