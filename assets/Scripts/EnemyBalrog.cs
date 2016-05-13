using UnityEngine;
using System.Collections;

public class EnemyBalrog : Enemy {

    public GameObject fireball;

    //Amount of time allowed for attacking
    public float attackingTime = 25f;
    //Amount of time allowed for being stunned
    public float hitTime = 15f;

    private Animator anim;

    //Is stunned check
    private bool isHit;
    //Is attacking check
    private bool isAttacking;
    //Time to next attack
    private float attackTime;
    //Time to next stun
    public float timeToHit;

    void Awake()
    {
        anim = GetComponent<Animator>();
        timeToHit = attackingTime;
    }

    void Update()
    {
        //If alive and there is a target
        if (health > 0 && player != null)
        {
            //If the player is in range, if we are not currently attacking and if we are not stunned
            if ((Vector2.Distance(player.transform.position, transform.position) < rangeCheck) && !isAttacking && timeToHit > 0)
            {
                isAttacking = true;
                anim.SetBool("IsAttacking", isAttacking);
                attackTime = 1.35f;
                Instantiate(fireball, new Vector3(transform.position.x + 3, transform.position.y + 5, 4.5f), Quaternion.Euler(0,0,-25));
                timeToHit -= Time.deltaTime;
            }
            //If we are stunned
            else if (timeToHit < 0)
            {
                isHit = true;
                anim.SetBool("IsHit", isHit);
                timeToHit -= Time.deltaTime;
            }
            //If we are finished attacking
            else if (attackTime < 0)
            {
                isAttacking = false;
                anim.SetBool("IsAttacking", isAttacking);
            }
            //If the stun is up
            if (timeToHit < -hitTime)
            {
                //Reset time to stun
                timeToHit = attackingTime;
                isHit = false;
                anim.SetBool("IsHit", isHit);
            }
            //If we are currently attacking
            if (attackTime > 0)
            {
                attackTime -= Time.deltaTime;
                timeToHit -= Time.deltaTime;
            }
            else
            {
                //Reset attacking
                isAttacking = false;
            }

        }
        else
        {
            StartCoroutine(Die());
        }
    }
}
