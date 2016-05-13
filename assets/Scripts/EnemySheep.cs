using UnityEngine;
using System.Collections;

public class EnemySheep : Enemy {

    private float waitTime;
    private bool currentlyCharging;
    private int chargeDirection;
    private float chargeTime;

    private Animator anim;

    private float deathTime;

    void Awake () {
        anim = GetComponent<Animator>();
        currentlyCharging = false;
        waitTime = 0f;
        deathTime = 0f;
	}
	
	void Update () {
        //If alive and there is a player
        if (health > 0)
        {
            if (player != null && !player.GetComponent<PlayerController>().isHide)
            {
                //If in range
                if (Vector2.Distance(player.transform.position, transform.position) < rangeCheck)
                {
                    //If we're chilling
                    if (!currentlyCharging && (waitTime <= 0))
                    {
                        //Charge!
                        currentlyCharging = true;
                        anim.SetBool("IsCharging", currentlyCharging);
                        chargeTime = Time.time + 1.5f;
                        if (player.transform.position.x < transform.position.x)
                        {
                            chargeDirection = -1;
                        }
                        else
                        {
                            chargeDirection = 1;
                        }
                    }
                    //If we're already charging 
                    else if (currentlyCharging)
                    {
                        if (chargeTime > Time.time)
                        {
                            transform.position = new Vector3(transform.position.x + (chargeDirection * Time.deltaTime * speed), transform.position.y, transform.position.z);
                        }
                        else
                        {
                            //Set wait 
                            currentlyCharging = false;
                            waitTime = 2f;
                            anim.SetBool("IsCharging", currentlyCharging);
                        }

                    }
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

            }
                //If we're waiting
                if (waitTime > 0)
                {
                    waitTime -= Time.deltaTime;
                }
                else
                {
                    anim.SetBool("IsHit", false);
                }
            

        }
        else
        {
            StartCoroutine(Die());
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (currentlyCharging && (coll.gameObject.tag == "Player"))
        {
            //Hit something
            currentlyCharging = false;
            waitTime = 2f;
            anim.SetBool("IsCharging", currentlyCharging);
            anim.SetBool("IsHit", true);
            coll.gameObject.SendMessage("TakeDamage", 10);
        }
    }
}
