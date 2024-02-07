using UnityEngine;


public class scrKey : MonoBehaviour
{
    public GameObject StateMagine;
    public Vector2 savePoint;
    GameObject player;
    lockAndKey scr;
    public bool Collected;
    CharacterController2D scrPlayer;

    public Transform keysUIElement;
    [SerializeField]
    private float lightIntensityUI, lightIntensityGeneral;
    public bool manual = false;

    public void Start()
    {
        this.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = lightIntensityGeneral;
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
            manual = true;
            collect();
        }
    }

    public void collect()
    {
        if (!Collected)
        {
            savePoint = transform.position;
        }

        this.transform.parent = keysUIElement;
        this.GetComponent<BoxCollider2D>().enabled = false;
        /*
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
        */
        Collected = true;
        GameObject.Find("KeysUI").GetComponent<KeyUISystems>().keys.Add(this.gameObject);
        this.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = lightIntensityUI;
        if (manual)
        {
            GameObject.Find("LevelResetter").GetComponent<levelState>().AddItemToList(this.gameObject);
        }
        
    }

    public void Reverd()
    {
        this.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = lightIntensityGeneral;
        GameObject.Find("KeysUI").GetComponent<KeyUISystems>().keys.Remove(this.gameObject);
        this.transform.parent = StateMagine.transform;
        transform.position = savePoint;
        Collected = false;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.SetActive(true);
    }
}
