using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireSystems : CampfireComponents, IInteractable
{

    [SerializeField] private AudioSource fireSFX;

    public void Start()
    {
        acceptablePlayerPos = false;
        playerCanToggle = true;
    }

    void Update()
    {


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        acceptablePlayerPos = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            acceptablePlayerPos = false;
    }

    public void ToggleOTA()
    {
        foreach (GameObject oTA in objectsToActivate)
        {
            oTA.SetActive(!oTA.activeSelf);
        }

        foreach (BoxCollider2D wellTrigger in wellTriggers)
        {
            wellTrigger.enabled = !wellTrigger.enabled;
        }

        playerCanToggle = true;
    }

    public void Interact()
    {
        if (playerCanToggle)
        {
            GameObject.Find("LevelResetter").GetComponent<levelState>().AddItemToList(this.gameObject);
            ToggleOTA();
            fireSFX.pitch = Random.RandomRange(0.85f, 1.2f);
            fireSFX.Play();
            playerCanToggle = false;
        }
    }
}
