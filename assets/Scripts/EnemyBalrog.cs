using UnityEngine;
using System.Collections;

public class EnemyBalrog : Enemy
{

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
        if (health > 0)
        {
            if (player != null &&
                !player.GetComponent<PlayerController>().isHide)
            {
                //If the player is in range, if we are not currently attacking 
                //and if we are not stunned
                if ((Vector2.Distance(player.transform.position,
                    transform.position) < rangeCheck) && !isAttacking &&
                    timeToHit > 0)
                {
                    //Set isAttacking to true
                    isAttacking = true;
                    //Set animation IsAttacking to the correct value
                    anim.SetBool("IsAttacking", isAttacking);
                    //Set the attack time
                    attackTime = 1.35f;
                    //Instantiate a fireball
                    Instantiate(fireball, new Vector3(transform.position.x + 3,
                        transform.position.y + 5, 4.5f),
                        Quaternion.Euler(0, 0, -25));
                    //Update time to hit
                    timeToHit -= Time.deltaTime;
                }
                //If we are stunned
                else if (timeToHit < 0)
                {
                    //Set isHit to true
                    isHit = true;
                    //Set animation IsHit to the correct value
                    anim.SetBool("IsHit", isHit);
                    //Update time to hit
                    timeToHit -= Time.deltaTime;
                }
                //If we are finished attacking
                else if (attackTime < 0)
                {
                    //Set isAttacking to false
                    isAttacking = false;
                    //Set animation IsAttacking to the correct value
                    anim.SetBool("IsAttacking", isAttacking);
                }
                //If the stun is up
                if (timeToHit < -hitTime)
                {
                    //Reset time to stun
                    timeToHit = attackingTime;
                    //Set isHit to false
                    isHit = false;
                    //Set animation IsHit to the correct value
                    anim.SetBool("IsHit", isHit);
                }
                //If we are currently attacking
                if (attackTime > 0)
                {
                    //Update attack time
                    attackTime -= Time.deltaTime;
                    //Update timeToHit
                    timeToHit -= Time.deltaTime;
                }
                else
                {
                    //Reset attacking
                    isAttacking = false;
                }
            }

        }
        else
        {
            //Start Die
            StartCoroutine(Die());
        }
    }
}