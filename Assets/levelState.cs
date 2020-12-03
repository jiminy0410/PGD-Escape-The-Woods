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
    private int itemsAtThisPointInTime;
    [SerializeField]
    private bool newPointInTime = false;

    void Start()
    {

    }


    void Update()
    {

        //for (int i = 0; i < items.Count; i++)
        //{
        //    if (items[i].GetComponent<scrKey>() != null)
        //    {
        //        if (items[i].GetComponent<scrKey>().Collected == true)
        //        {
        //            if (!itemspassed[i])
        //                currentPointInTime++;
        //            itemspassed[i] = true;
        //        }
        //    }
        //    if (items[i].GetComponent<lockAndKey>() != null)
        //    {
        //        if (items[i].GetComponent<lockAndKey>().kickingDownTheDoor == true)
        //        {
        //            if (!itemspassed[i])
        //                currentPointInTime++;
        //            itemspassed[i] = true;
        //        }
        //    }
        //    if (items[i].GetComponent<DoubleJump>() != null)
        //    {
        //        if (items[i].GetComponent<DoubleJump>().Collected == true)
        //        {
        //            if (!itemspassed[i])
        //                currentPointInTime++;
        //            itemspassed[i] = true;
        //        }
        //    }
        //}



        if(items.Count > itemsAtThisPointInTime && !newPointInTime)
        {
            currentPointInTime++;
            newPointInTime = true;
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

        ResetPointInTime();

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

        ClearTheList();
    }

    public void AddItemToList(GameObject item)
    {
        items.Add(item);
        itemspassed.Add(true);
    }

    public void ClearTheList()
    {
        for (int i = items.Count - 1; i >= itemsAtThisPointInTime; i--)
        {
            if (itemspassed[i] == false)
            {
                itemspassed.RemoveAt(i);
                items.Remove(items[i]);
                ClearTheList();
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
        itemsAtThisPointInTime = items.Count;
        newPointInTime = false;
    }
}
