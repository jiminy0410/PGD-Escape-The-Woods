using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUISystems : KeyUIComponents
{

    void Update()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            keys[i].transform.parent = this.transform;
            keys[i].transform.position = new Vector2(this.transform.position.x + i * 0.75f, this.transform.position.y); 
        }
    }
}
