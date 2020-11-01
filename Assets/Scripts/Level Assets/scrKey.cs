using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrKey : MonoBehaviour
{
    public GameObject StateMagine;
    GameObject player;
    lockAndKey scr;
    CharacterController2D scrPlayer;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scrPlayer = player.GetComponent<CharacterController2D>();
        scr = StateMagine.GetComponent<lockAndKey>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.transform.parent = collision.transform;
            if (scrPlayer.m_FacingRight)
            {
                transform.position = new Vector2(collision.transform.position.x + 0.4f, collision.transform.position.y);
                Vector3 theScale = transform.localScale;
                theScale.y *= -1;
                transform.localScale = theScale;
            }
            else
            {
                transform.position = new Vector2(collision.transform.position.x - 0.4f, collision.transform.position.y);
            }
            scr.hasKey = true;
        }
    }
}
