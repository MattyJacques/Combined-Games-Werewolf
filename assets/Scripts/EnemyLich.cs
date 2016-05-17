using UnityEngine;
using System.Collections;

public class EnemyLich : Enemy
{

    //Reference to the animator
    private Animator anim;
    //Reference to the rigidbody
    private Rigidbody2D rb;

    //Is moving check
    private bool isMoving;
    //Is attacking check
    private bool isAttacking;
    //Time to next attack 
    private float attackTime;


    void Awake()
    {
        //Set the animator
        anim = GetComponent<Animator>();
        //Set the rigidbody
        rb = GetComponent<Rigidbody2D>();
        //Set is moving to dalse initally
        isMoving = false;
    }

    void Update()
    {
        //If alive 
        if (health > 0)
        {
            //If there is a player and they are not hiding
            if (player != null &&
                !player.GetComponent<PlayerController>().isHide)
            {
                //If the player is close and we are not currently attacking
                if ((Vector2.Distance(player.transform.position,
                    transform.position) < 2) && !isAttacking)
                {
                    //Attack
                    //Set attacking to true
                    isAttacking = true;
                    //Set the attack animation
                    anim.SetBool("IsAttacking", isAttacking);
                    //Set the attack timer
                    attackTime = 1.35f;
                }
                //If the player is within chasing distance
                else if (Vector2.Distance(player.transform.position,
                    transform.position) < rangeCheck)
                {
                    //Move towards the player
                    //Set moving to true
                    isMoving = true;
                    //Set the moving animation
                    anim.SetBool("IsMoving", isMoving);
                    //Add a force towards the player
                    rb.AddForce((player.transform.position -
                        transform.position) * speed * Time.deltaTime);
                }
                else
                {
                    //Slow speed when not in range
                    isMoving = false;
                    //Set the moving animation
                    anim.SetBool("IsMoving", isMoving);
                    //Reduce the speed
                    rb.AddRelativeForce(-rb.velocity * 0.95f);
                }


                //Look direction
                if ((player.transform.position.x < transform.position.x) &&
                    (transform.localScale.x < 0))
                {
                    //Set the sprite direction
                    transform.localScale = new Vector3(transform.localScale.x *
                        -1, transform.localScale.y, transform.localScale.z);
                }
                else if ((player.transform.position.x > transform.position.x)
                    && (transform.localScale.x > 0))
                {
                    //Set the sprite direction
                    transform.localScale = new Vector3(transform.localScale.x *
                        -1, transform.localScale.y, transform.localScale.z);
                }

                //If attack time has now reset
                if (attackTime > 0)
                {
                    //Reduce the attack time
                    attackTime -= Time.deltaTime;
                }
                else
                {
                    //Set attacking to false
                    isAttacking = false;
                    //Set the attack animation
                    anim.SetBool("IsAttacking", isAttacking);
                }
            }
        }
        else
        {
            //Start Die
            StartCoroutine(Die());
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //If we attack and hit the player
        if (isAttacking && (coll.gameObject.tag == "Player"))
        {
            //Damage the player
            coll.gameObject.SendMessage("TakeDamage", damage);
        }
    }
}