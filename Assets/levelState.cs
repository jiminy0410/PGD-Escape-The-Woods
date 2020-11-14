using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelState : MonoBehaviour
{
    public List<GameObject> items;
    public List<bool> itemspassed;
    public int pointInTime;
    public int currentPointInTime;

    void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            itemspassed.Add(false);
        }
    }


    void Update()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetComponent<scrKey>() != null)
            {
                if (items[i].GetComponent<scrKey>().Collected == true)
                {
                    if (!itemspassed[i])
                        currentPointInTime++;
                    itemspassed[i] = true;
                }
            }
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetComponent<lockAndKey>() != null)
            {
                if (items[i].GetComponent<lockAndKey>().kickingDownTheDoor == true)
                {
                    if (!itemspassed[i])
                        currentPointInTime++;
                    itemspassed[i] = true;
                }
            }
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetComponent<DoubleJump>() != null)
            {
                if (items[i].GetComponent<DoubleJump>().Collected == true)
                {
                    if (!itemspassed[i])
                        currentPointInTime++;
                    itemspassed[i] = true;
                }
            }
        }
    }

    public void Rvert()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i >= pointInTime)
            {
                itemspassed[i] = false;
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (itemspassed[i] == false)
            {
                if (items[i].GetComponent<lockAndKey>() != null)
                {
                    items[i].GetComponent<lockAndKey>().Reverd();
                }
                if (items[i].GetComponent<scrKey>() != null)
                {
                    items[i].GetComponent<scrKey>().Reverd();
                }
                if (items[i].GetComponent<DoubleJump>() != null)
                {
                    items[i].GetComponent<DoubleJump>().Reverd();
                }
            }
        }
        currentPointInTime = pointInTime;
    }
}
