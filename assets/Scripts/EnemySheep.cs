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
        if (health > 0)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < rangeCheck)
            {
                if (!currentlyCharging && (waitTime <= 0))
                {
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
                else if (currentlyCharging)
                {
                    if (chargeTime > Time.time)
                    {
                        transform.position = new Vector3(transform.position.x + (chargeDirection * Time.deltaTime * speed), transform.position.y, transform.position.z);
                    }
                    else
                    {
                        currentlyCharging = false;
                        waitTime = 2f;
                        anim.SetBool("IsCharging", currentlyCharging);
                    }

                }
            }
            else
            {
                //Stand
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
            if(deathTime == 0)
            {
                deathTime = Time.time + 2f;
                anim.SetBool("IsHit", false);
                anim.SetBool("IsDead", true);
            }
            else if (Time.time > deathTime)
            {
                Destroy(this.gameObject);
            }       
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
            //coll.gameObject.GetComponent<Player>().health -= x;
        }
    }
}
