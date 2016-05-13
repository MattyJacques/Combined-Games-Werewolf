using UnityEngine;
using System.Collections;

public class EnemyLich : Enemy {

    private Animator anim;
    private Rigidbody2D rb;

    private bool isMoving;
    private bool isAttacking;
    private float attackTime;

    private float deathTime;

    void Awake () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isMoving = false;
    }
	
	void Update () {
        //If alive and there is a player
        if (health > 0 && player != null)
        {
            //If the player is close and we are not currently attacking
            if ((Vector2.Distance(player.transform.position, transform.position) < 2) && !isAttacking)
            {
                //Attack
                isAttacking = true;
                anim.SetBool("IsAttacking", isAttacking);
                attackTime = 1.35f;
            }
            //If the player is within chasing distance
            else if (Vector2.Distance(player.transform.position, transform.position) < rangeCheck)
            {
                //Move towards the player
                isMoving = true;
                anim.SetBool("IsMoving", isMoving);
                rb.AddForce((player.transform.position - transform.position) * speed * Time.deltaTime);
            }
            else
            {
                //Slow speed when not in range
                isMoving = false;
                anim.SetBool("IsMoving", isMoving);
                rb.AddRelativeForce(-rb.velocity * 0.95f);
            }

            //Look direction
            if ((player.transform.position.x < transform.position.x) && (transform.localScale.x < 0))
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if ((player.transform.position.x > transform.position.x) && (transform.localScale.x > 0))
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            if (attackTime > 0)
            {
                attackTime -= Time.deltaTime;
            }
            else
            {
                isAttacking = false;
                anim.SetBool("IsAttacking", isAttacking);
            }
        }
        else
        {
            StartCoroutine(Die());
        }
    }
}
