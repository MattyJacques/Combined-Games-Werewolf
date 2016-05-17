using UnityEngine;
using System.Collections;

public class EnemySheep : Enemy {

    //Wait time vairable
    private float waitTime;
    //Currently charging check
    private bool currentlyCharging;
    //Charge direction
    private int chargeDirection;
    //Time to charge
    private float chargeTime;

    //Reference to animator
    private Animator anim;
    //Time to death
    private float deathTime;

    void Awake () {
        //Set the animator
        anim = GetComponent<Animator>();
        //Set currently charging
        currentlyCharging = false;
        //Set the wait time
        waitTime = 0f;
        //Set the death time
        deathTime = 0f;
	}
	
	void Update () {
        //If alive
        if (health > 0)
        {
            //If there is a player and they are not hiding
            if (player != null && 
                !player.GetComponent<PlayerController>().isHide)
            {
                //If in range
                if (Vector2.Distance(player.transform.position, 
                    transform.position) < rangeCheck)
                {
                    //If we're chilling
                    if (!currentlyCharging && (waitTime <= 0))
                    {
                        //Charge!
                        currentlyCharging = true;
                        //Set animation
                        anim.SetBool("IsCharging", currentlyCharging);
                        //Set charge time
                        chargeTime = Time.time + 1.5f;
                        //Check direction of the player
                        if (player.transform.position.x < transform.position.x)
                        {
                            //Set the charge direction
                            chargeDirection = -1;
                        }
                        else
                        {
                            //Set the charge direction
                            chargeDirection = 1;
                        }
                    }
                    //If we're already charging 
                    else if (currentlyCharging)
                    {
                        //If we are still charging 
                        if (chargeTime > Time.time)
                        {
                            //Move the sheep in the correct direction
                            transform.position = new Vector3(
                                transform.position.x + (chargeDirection * 
                                Time.deltaTime * speed), transform.position.y, 
                                transform.position.z);
                        }
                        else
                        {
                            //Set wait 
                            currentlyCharging = false;
                            //Set wait time to 2 seconds
                            waitTime = 2f;
                            //Set wait animation
                            anim.SetBool("IsCharging", currentlyCharging);
                        }

                    }
                }

                //Look direction
                if ((player.transform.position.x < transform.position.x) && 
                    (transform.localScale.x < 0))
                {
                    //Set sprite direction
                    transform.localScale = new Vector3(transform.localScale.x *
                        -1, transform.localScale.y, transform.localScale.z);
                }
                else if ((player.transform.position.x > transform.position.x) 
                    && (transform.localScale.x > 0))
                {
                    //Set sprite direction
                    transform.localScale = new Vector3(transform.localScale.x *
                        -1, transform.localScale.y, transform.localScale.z);
                }

            }
                //If we're waiting
                if (waitTime > 0)
                {
                //Reduce the wait time
                    waitTime -= Time.deltaTime;
                }
                else
                {
                //Set the hit animation
                    anim.SetBool("IsHit", false);
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
        if (currentlyCharging && (coll.gameObject.tag == "Player"))
        {
            //Stun the sheep
            currentlyCharging = false;
            //Set the wait time
            waitTime = 2f;
            //Set the animation
            anim.SetBool("IsCharging", currentlyCharging);
            anim.SetBool("IsHit", true);
            //Damage the player
            coll.gameObject.SendMessage("TakeDamage", damage);
        }
    }
}
