using System.Collections;
using UnityEngine;

public class LifetimeDim : MonoBehaviour
{
    //this is some code Yves made for another game. basically, the object this is assigned to, now has a timer.
    //depending on a couple of settings, you can change how long the timer lasts, how many times it will blink during the timer and what colors it wil have during the change
    private float tint = 0;
    public float lifeTime;

    private bool biteable = false;

    public float blinks;
    public float colorIntensity;
    private float counter;
    public GameObject player;
    public Transform respawnPoint;

    public Color from, to;
    // Start is called before the first frame update

    [SerializeField]
    private AudioSource chargeSFX, attackSFX;
    [SerializeField]
    private Sprite idleSpr, activeSpr;

    void Start()
    {
        counter = blinks;
        colorIntensity = colorIntensity / 100;
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            enabled = true;

            chargeSFX.Play();
            chargeSFX.pitch += 0.1f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            biteable = true;
        }
    }

    void FixedUpdate()
    {

        tint += 0.02f * (colorIntensity) * (blinks / lifeTime);
        //image.r = tint;
        GetComponent<SpriteRenderer>().color = Color.Lerp(from, to, tint);
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1);
        if (tint >= 1 * colorIntensity)
        {
            chargeSFX.Play();
            chargeSFX.pitch += 0.1f;

            counter--;
            tint = 0;

        }
        if (counter == 0)
        {
            //player.GetComponent<FuelBasedLight>().currentFuel = player.GetComponent<FuelBasedLight>().maxFuel;

            attackSFX.Play();
            chargeSFX.pitch = 1;

            GetComponent<SpriteRenderer>().sprite = activeSpr;


            if (biteable)
            {
                GameObject.Find("DeathPit").GetComponent<DeathPit>().Death();
            }

            counter = blinks;
            GetComponent<SpriteRenderer>().color = Color.white;
            StartCoroutine("returnSprite");
            enabled = false;
        }
        biteable = false;

        //Debug.Log("ye boi");
    }

    IEnumerator returnSprite()
    {
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().sprite = idleSpr;
    }
}
