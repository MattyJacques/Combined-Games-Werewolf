using UnityEngine;
using System.Collections;

public class EnemyBalrogFire : MonoBehaviour {

    public float seconds = 2.0f;
    public float fadeSeconds = 1.0f;

    private Rigidbody2D rb;
    private Animator anim;
    public bool isDead;

    private SpriteRenderer spriteRenderer;
    public GameObject player;
    public Vector3 direction;

    void Start ()
    {
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.position - transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isDead)
        {
            rb.AddForce(direction * 50 * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("IsDead", isDead);
            //Reduce the remaining life time of the game object
            seconds -= Time.deltaTime;

            //If the time has experired
            if (seconds <= 0.0f)
            {
                Destroy(gameObject);
            }
            else
            {
                //Get the sprite renderer component
                if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

                //Set the colour
                var color = spriteRenderer.color;

                //Reduce the alpha over time
                color.a = Mathf.Clamp01(Zero(fadeSeconds) == false ? seconds / fadeSeconds : 0.0f);

                //Set the new colour (alpha)
                spriteRenderer.color = color;
            }
        }
	}

    public static bool Zero(float v)
    {
        return v == 0.0f;
        //return Mathf.Approximately(v, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enviroment")
        {
            isDead = true;
        }
        else if (coll.gameObject.tag == "Player")
        {
            //Player take damage
        }
    }
}
