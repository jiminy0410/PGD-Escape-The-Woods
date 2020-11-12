using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    GameObject player;
    CharacterController2D doubleJumpPlayer;
    public ParticleSystem acquisitonEffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        doubleJumpPlayer = player.GetComponent<CharacterController2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doubleJumpPlayer.Max_Jumps = 1;
            acquisitonEffect.transform.position = this.transform.position;
            acquisitonEffect.Play();
            acquisitonEffect.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
