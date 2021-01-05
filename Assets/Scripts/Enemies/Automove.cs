using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automove : MonoBehaviour
{
    public Transform rayPoint;
    public float rayLength = 0.1f;

    public LayerMask collisionMask;

    public float speed = 2f;

    private Vector2 deltaDist;
    RaycastHit2D pathingRay;

    // Start is called before the first frame update
    // make sure to set speed before running the script
    void Start()
    {
        speed /= -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //GameObject.Find("DeathPit").GetComponent<DeathPit>().Death();
            Debug.Log("blueh, Im ded");
        }

        if(collision.gameObject.layer == 10)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(deltaDist, this.transform.position) < 0.0001f)
        {
            //turn around and walk away
            Flip();
        }

        deltaDist = this.transform.position;

        pathingRay = Physics2D.Raycast(rayPoint.position, Vector2.down, rayLength, collisionMask);

        if (pathingRay.collider != null)
        {
            //moveboi
            Debug.Log(pathingRay.collider.name);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

            //if(this.GetComponent<Rigidbody2D>().velocity.x < 0.001f && this.GetComponent<Rigidbody2D>().velocity.x > -0.001f)
            //{
            //    Vector3 lol = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            //    //turn around and walk away
            //    transform.localScale = lol;
            //    speed *= -1;
            //}
        }
        else
        {
            //turn around and walk away
            Flip();
        }
    }

    void Flip()
    {
        Vector3 lol = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        transform.localScale = lol;
        speed *= -1;
    }
}
