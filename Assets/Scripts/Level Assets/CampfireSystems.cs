using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireSystems : CampfireComponents
{

    void Update()
    {
        if (acceptablePlayerPos && Input.GetAxisRaw("Vertical") > 0)
        {
            foreach (GameObject oTA in objectsToActivate)
            {
                oTA.SetActive(true);
            }

            foreach (BoxCollider2D wellTrigger in wellTriggers)
            {
                wellTrigger.enabled = true;
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        acceptablePlayerPos = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        acceptablePlayerPos = false;
    }
}
