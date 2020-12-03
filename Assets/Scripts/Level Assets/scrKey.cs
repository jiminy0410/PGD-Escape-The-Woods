using UnityEngine;

public class scrKey : MonoBehaviour
{
    public GameObject StateMagine;
    public Vector2 savePoint;
    GameObject player;
    lockAndKey scr;
    public bool Collected;
    CharacterController2D scrPlayer;
    public void Start()
    {
        transform.parent = StateMagine.transform;
        savePoint = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        scrPlayer = player.GetComponent<CharacterController2D>();
        scr = StateMagine.GetComponent<lockAndKey>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!Collected)
            {
                savePoint = transform.position;
            }

            this.transform.parent = collision.transform;
            if (scrPlayer.FacingRight)
            {
                for (int i = 0; i < scr.keys.Count; i++)
                {
                    if (scr.keys[i] == this.gameObject)
                    {
                        transform.position = new Vector2(collision.transform.position.x + 0.4f * i, collision.transform.position.y);
                    }
                }
                Vector3 theScale = transform.localScale;
                theScale.y *= -1;
                transform.localScale = theScale;
            }
            else
            {
                for (int i = 0; i < scr.keys.Count; i++)
                {
                    if (scr.keys[i] == this.gameObject)
                    {
                        transform.position = new Vector2(collision.transform.position.x - 0.4f * i, collision.transform.position.y);
                    }
                }
            }
            Collected = true;
            GameObject.Find("LevelResetter").GetComponent<levelState>().AddItemToList(this.gameObject);
        }
    }

    public void Reverd()
    {
        this.transform.parent = StateMagine.transform;
        transform.position = savePoint;
        Collected = false;
        this.gameObject.SetActive(true);
    }
}
