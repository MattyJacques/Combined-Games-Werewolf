using UnityEngine;
using System.Collections;

public class EnemyBalrog : Enemy {

    public GameObject fireball;

    public float attackingTime = 25f;
    public float hitTime = 15f;

    private Animator anim;
    private Rigidbody2D rb;

    private bool isHit;
    private bool isAttacking;
    private float attackTime;
    public float timeToHit;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timeToHit = attackingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            if ((Vector2.Distance(player.transform.position, transform.position) < rangeCheck) && !isAttacking && timeToHit > 0)
            {
                isAttacking = true;
                anim.SetBool("IsAttacking", isAttacking);
                attackTime = 1.35f;
                Instantiate(fireball, new Vector3(transform.position.x + 3, transform.position.y + 5, 4.5f), Quaternion.Euler(0,0,-25));
                
            }
            else if (timeToHit < 0)
            {
                isHit = true;
                anim.SetBool("IsHit", isHit);
            }

            if (timeToHit < -hitTime)
            {
                timeToHit = attackingTime;
                isHit = false;
                anim.SetBool("IsHit", isHit);
            }
            else
            {
                timeToHit -= Time.deltaTime;
            }

            if (attackTime > 0)
            {
                attackTime -= Time.deltaTime;
            }
            else
            {
                isAttacking = false;
            }

        }
        else
        {

        }
    }
}
