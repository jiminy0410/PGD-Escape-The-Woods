using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    GameObject player;
    CharacterController2D doubleJumpPlayer;
    public ParticleSystem acquisitonEffect;
    public bool Collected;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        doubleJumpPlayer = player.GetComponent<CharacterController2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
            acquisitonEffect.transform.position = this.transform.position;
            acquisitonEffect.Play();
            acquisitonEffect.GetComponent<AudioSource>().Play();
            GameObject.Find("LevelResetter").GetComponent<levelState>().AddItemToList(this.gameObject);
        }
    }

    public void Collect()
    {
        doubleJumpPlayer.Max_Jumps = 1;
        this.gameObject.SetActive(false);
        Collected = true;
    }
    public void Reverd()
    {
        doubleJumpPlayer.Max_Jumps = 0;
        this.gameObject.SetActive(true);
        Collected = false;
    }
}
