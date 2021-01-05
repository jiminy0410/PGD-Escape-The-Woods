using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLock : MonoBehaviour
{
    public GameObject StateMagine;
    public lockAndKey scr;
    public bool manual = false;

    public void Start()
    {
        StateMagine = this.gameObject;
        scr = this.GetComponent<lockAndKey>();
    }

    // todo: manual yeetus deletus.


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && scr.hasKeys)
        {
            manual = true;
            useKey();
        }
    }

    public void useKey()
    {
        this.gameObject.SetActive(false);
        scr.kickingDownTheDoor = true;

        foreach (GameObject item in scr.keys)
        {
            item.SetActive(false);
            GameObject.Find("KeysUI").GetComponent<KeyUISystems>().keys.Remove(item);
            if (manual)
            {
                GameObject.Find("LevelResetter").GetComponent<levelState>().AddItemToList(this.gameObject);
            }
            //item.transform.parent = StateMagine.transform;
        }
    }
}
