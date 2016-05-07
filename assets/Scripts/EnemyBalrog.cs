using UnityEngine;
using System.Collections;

public class EnemyBalrog : Enemy {

    private Animator anim;
    private Rigidbody2D rb;

    private bool isMoving;
    private bool isAttacking;
    private float attackTime;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
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

            }
        }
        else
        {

        }
    }
}
