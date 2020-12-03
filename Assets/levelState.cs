using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelState : MonoBehaviour
{
    public List<GameObject> items;
    public List<bool> itemspassed;
    public int pointInTime;
    public int currentPointInTime;

    [SerializeField]
    private int itemsAtThisPointInTime = 0;
    [SerializeField]
    private bool newPointInTime = false;

    void Start()
    {

    }


    void Update()
    {

        if (items.Count > itemsAtThisPointInTime && !newPointInTime)
        {
            currentPointInTime++;
            newPointInTime = true;
        }

    }

    public void Rvert()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i >= itemsAtThisPointInTime)
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

        ResetPointInTime();

        ClearTheList();
    }

    public void AddItemToList(GameObject item)
    {
        items.Add(item);
        itemspassed.Add(true);
    }

    public void ClearTheList()
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (itemspassed[i] == false)
            {
                itemspassed.RemoveAt(i);
                items.Remove(items[i]);
                itemsAtThisPointInTime = items.Count;
            }
        }
    }

    public void SavePointInTime()
    {
        pointInTime = currentPointInTime;
        itemsAtThisPointInTime = items.Count;
        newPointInTime = false;
    }

    public void ResetPointInTime()
    {
        currentPointInTime = pointInTime;
        newPointInTime = false;
    }
}
