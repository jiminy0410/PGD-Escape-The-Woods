using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorComponents : MonoBehaviour
{
    public bool finalDoor;

    [Space]

    public Transform nextDoor;

    [HideInInspector]
    public GameObject player;

    [Space]
    [HideInInspector]
    public float cooldownCount;
    public float cooldownTime;

    [HideInInspector]
    public GameObject levelManager;
}
