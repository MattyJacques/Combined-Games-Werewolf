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
        if (health > 0 && player != null)
        {
            if ((Vector2.Distance(player.transform.position, transform.position) < 2) && !isAttacking)
            {
                isAttacking = true;
                anim.SetBool("IsAttacking", isAttacking);
                attackTime = 1.35f;
            }
            else if (Vector2.Distance(player.transform.position, transform.position) < rangeCheck)
            {
                isMoving = true;
                anim.SetBool("IsMoving", isMoving);
                rb.AddForce((player.transform.position - transform.position) * speed * Time.deltaTime);
            }
            else
            {
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

            if (deathTime == 0)
            {
                deathTime = Time.time + 2f;
                rb.gravityScale = 1f;
                anim.SetBool("IsDead", true);
            }
            else if (Time.time > deathTime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
