using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automove : MonoBehaviour
{
    public Transform rayPoint;
    public float rayLength = 0.1f;

    public float speed = 1;

    // Start is called before the first frame update
    // make sure to set speed before running the script
    void Start()
    {
        speed /= -100;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //GameObject.Find("DeathPit").GetComponent<DeathPit>().Death();
            Debug.Log("blueh, Im ded");
        }
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D pathingRay = Physics2D.Raycast(rayPoint.position, Vector2.down, rayLength);

        if (pathingRay.collider != null)
        {
            //moveboi
            transform.position += new Vector3(speed, 0, 0);
        }
        else
        {
            Vector3 lol = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            //turn around and walk away
            transform.localScale = lol;
            speed *= -1;
        }
        if (speed>0)
        {
            pathingRay = Physics2D.Raycast(rayPoint.position, Vector2.right, rayLength*1.5f);
        }
        else
        {
            pathingRay = Physics2D.Raycast(rayPoint.position, Vector2.left, rayLength*1.5f);
        }
      
    }
}
