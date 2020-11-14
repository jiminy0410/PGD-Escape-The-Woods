using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockAndKey : MonoBehaviour
{
    public bool hasKeys;
    public bool kickingDownTheDoor;
    public List<GameObject> keys;
    public List<bool> keyCollected;

    void Start()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            keyCollected.Add(false);
        }
    }


    void Update()
    {
        if (keyCollected.TrueForAll(b => b))
        {
            hasKeys = true;
        }
        else
        {
            hasKeys = false;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i].GetComponent<scrKey>().Collected == true)
            {
                keyCollected[i] = true;
            }
        }
    }

    public void Reverd()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i].GetComponent<scrKey>().Collected == false)
            {
                keyCollected[i] = false;
            }
            else
            {
                keys[i].SetActive(true);
            }
        }
        kickingDownTheDoor = false;
        this.gameObject.SetActive(true);
    }
}
