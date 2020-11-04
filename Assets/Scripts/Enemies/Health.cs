using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when this script is enabled, it will lower health by 1 and then disable itself. add stuff before the disabling to change what being damaged means.
        health--;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        this.enabled = false;
    }
}
