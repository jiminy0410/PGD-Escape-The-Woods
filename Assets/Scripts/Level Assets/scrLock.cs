using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLock : MonoBehaviour
{
    public GameObject StateMagine;
    public lockAndKey scr;
    public void Start()
    {
        scr = StateMagine.GetComponent<lockAndKey>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && scr.hasKey)
        {
            this.gameObject.SetActive(false);
            scr.kickingDownTheDoor = true;
            scr.Key.SetActive(false);
            scr.Key.transform.parent = StateMagine.transform;
        }
    }
}
