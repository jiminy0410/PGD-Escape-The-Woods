using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireComponents : MonoBehaviour
{



    public List<GameObject> objectsToActivate;
    public List<BoxCollider2D> wellTriggers;

    [HideInInspector]
    public bool acceptablePlayerPos;

}
