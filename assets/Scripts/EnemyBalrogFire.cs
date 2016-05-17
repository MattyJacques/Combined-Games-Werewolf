using UnityEngine;
using System.Collections;

public class EnemyBalrogFire : MonoBehaviour {

    //Time to be alive
    public float seconds = 2.0f;
    //Time to fade
    public float fadeSeconds = 1.0f;

    //Reference to rigidbody
    private Rigidbody2D rb;
    //Reference to animator
    private Animator anim;

    //Is dead check
    public bool isDead;

    //Reference to sprite renderer
    private SpriteRenderer spriteRenderer;
    //Reference to the player
    public GameObject player;
    //Vector to hold directon
    public Vector3 direction;

    void Start ()
    {
        //Set inital dead state
        isDead = false;
        //Set rb to the relevent component
        rb = GetComponent<Rigidbody2D>();
        //Set anim to the relevent component
        anim = GetComponent<Animator>();
        //Set the player
        player = GameObject.FindGameObjectWithTag("Player");
        //Get direction to player
        direction = player.transform.position - transform.position;
    }
	
	void Update () {
        //Check if alive
        if (!isDead)
        {
            //Add force towards where the player was
            rb.AddForce(direction * 50 * Time.deltaTime);
        }
        else
        {
            //Stop all movement
            rb.velocity = new Vector2(0, 0);
            //Set IsDead animation
            anim.SetBool("IsDead", isDead);
            //Reduce the remaining life time of the game object
            seconds -= Time.deltaTime;

            //If the time has experired
            if (seconds <= 0.0f)
            {
                //Destroy the fireball
                Destroy(gameObject);
            }
            else
            {
                //Fade out before death
                //Get the sprite renderer component
                if (spriteRenderer == null) spriteRenderer = 
                        GetComponent<SpriteRenderer>();

                //Set the colour
                var color = spriteRenderer.color;

                //Reduce the alpha over time
                color.a = Mathf.Clamp01(Zero(fadeSeconds) == 
                    false ? seconds / fadeSeconds : 0.0f);

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
        //Check for hitting the ground/wall
        if (coll.gameObject.tag == "Enviroment")
        {
            isDead = true;
        }
        //Check for hitting the player
        else if (coll.gameObject.tag == "Player")
        {
            //Apply damage
            coll.gameObject.SendMessage("TakeDamage", 10);
        }
    }
}
