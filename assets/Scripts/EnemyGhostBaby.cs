using UnityEngine;
using System.Collections;

public class EnemyGhostBaby : Enemy
{

    private Animator anim;          //animator
    private Rigidbody2D rb;         //rigidbody

    private bool isMoving;          //bool is oving for animator
    private bool isAttacking;       //bool is attacking for animator
    private float attackTime;       //time to attack

    private float deathTime;        //death time

    void Awake()
    {
        anim = GetComponent<Animator>(); //set up animator
        rb = GetComponent<Rigidbody2D>();//set up rigid body
        isMoving = false;                // is it moving
    }

    void Update()
    {
        //If alive and there is a player
        if (health > 0)
        { //if player is not null and not hiding act
            if (player != null && 
                !player.GetComponent<PlayerController>().isHide)
            {
                //If the player is close and we are not currently attacking
                if ((Vector2.Distance(player.transform.position, 
                    transform.position) < 2) && !isAttacking)
                {
                    //Attack
                    isAttacking = true;
                    anim.SetBool("IsAttacking", isAttacking);
                    attackTime = 1.35f; 
                    //set bool and animator, set attack time
                }
                //If the player is within chasing distance
                else if (Vector2.Distance(player.transform.position, 
                    transform.position) < rangeCheck)
                {
                    //Move towards the player
                    isMoving = true;
                    anim.SetBool("IsMoving", isMoving);
                    rb.AddForce((player.transform.position - 
                        transform.position) * speed * Time.deltaTime);
                    //add force to move, set move animator
                }
                else
                {
                    //Slow speed when not in range
                    isMoving = false;
                    anim.SetBool("IsMoving", isMoving);
                    rb.AddRelativeForce(-rb.velocity * 0.95f);
                }


                //Look direction left or right 
                if ((player.transform.position.x < transform.position.x) &&
                    (transform.localScale.x < 0))
                {
                    transform.localScale = new Vector3(transform.localScale.x *
                        -1, transform.localScale.y, transform.localScale.z);
                }
                else if ((player.transform.position.x > transform.position.x) 
                    && (transform.localScale.x > 0))
                {
                    transform.localScale = new Vector3(transform.localScale.x *
                        -1, transform.localScale.y, transform.localScale.z);
                }
                //if attack time is grater than 0, reduce
                if (attackTime > 0)
                {
                    attackTime -= Time.deltaTime;
                }
                else
                {
                    //if less than, attack
                    isAttacking = false;
                    anim.SetBool("IsAttacking", isAttacking);
                }
            }
        }
        else
        {
            //if no health, die
            StartCoroutine(Die());
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        //on collision with player, deal damage. 
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("TakeDamage", 10);

        }
    }
}
