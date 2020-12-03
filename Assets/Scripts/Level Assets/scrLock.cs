﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLock : MonoBehaviour
{
    public GameObject StateMagine;
    public lockAndKey scr;
    public void Start()
    {
        StateMagine = this.gameObject;
        scr = StateMagine.GetComponent<lockAndKey>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && scr.hasKeys)
        {
            this.gameObject.SetActive(false);
            scr.kickingDownTheDoor = true;
            foreach (GameObject item in scr.keys)
            {
                item.SetActive(false);
                GameObject.Find("LevelResetter").GetComponent<levelState>().AddItemToList(this.gameObject);

                //item.transform.parent = StateMagine.transform;
            }

        }
    }
}
