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

    bool first;

    private void LateUpdate()
    {
        if (!first)
        {
            ObjectData data = SaveSystem.LoadObjects();

            if (data == null)
            {
                Debug.Log("I don't have anything...");
                return;
            }

            Debug.Log("Reorganizing stuff...");
            for (int i = 0; i < data.objects.Length; i++)
            {
                AddItemToList(GameObject.Find(data.objects[i]));
            }

            Debug.Log("Looks like I have:");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetComponent<scrLock>() != null)
                {
                    Debug.Log("destroyed this tree: " + items[i].name);
                    items[i].GetComponent<scrLock>().useKey();
                }
                if (items[i].GetComponent<scrKey>() != null)
                {
                    Debug.Log("got this key: " + items[i].name);
                    items[i].GetComponent<scrKey>().collect();
                }
                if (items[i].GetComponent<DoubleJump>() != null)
                {
                    Debug.Log("got this power" + items[i].name);
                    items[i].GetComponent<DoubleJump>().Collect();
                }
                if (items[i].GetComponent<CampfireSystems>() != null)
                {
                    Debug.Log("turned on this fire " + items[i].name);
                    items[i].GetComponent<CampfireSystems>().ToggleOTA();
                    items[i].GetComponent<CampfireSystems>().playerCanToggle = false;
                }

            }
            Debug.Log("and that's all my stuff.");

            first = true;
        }
    }
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
                if (items[i].GetComponent<CampfireSystems>() != null)
                {
                    items[i].GetComponent<CampfireSystems>().ToggleOTA();
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
        SaveSystem.SaveObjects(this);
        newPointInTime = false;
    }

    public void ResetPointInTime()
    {
        currentPointInTime = pointInTime;
        newPointInTime = false;
    }
}
