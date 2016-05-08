using UnityEngine;
using System.Collections;

public class EnemyMatty : Enemy
{

    private Animator anim;
    private Rigidbody2D rb;

    private bool isHit;
    private bool isMoving;
    private bool isJumping;

    void Awake()
    {
        isHit = false;
        isMoving = false;
        isJumping = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (health > 0 && player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 2)
            {
                isMoving = false;
                anim.SetBool("IsMoving", isMoving);

                //Jumping
                if (player.GetComponent<PlayerController>().isJumping && !isJumping)
                {
                    rb.AddForce(new Vector2(0f, player.GetComponent<PlayerController>().jumpForce));
                    isJumping = true;
                }
            }
            else if (Vector2.Distance(player.transform.position, transform.position) < rangeCheck)
            {
                rb.AddForce((player.transform.position - transform.position) * speed * Time.deltaTime);
                isMoving = true;
                anim.SetBool("IsMoving", isMoving);
            }
            else //Out of range
            {
                isMoving = false;
                anim.SetBool("IsMoving", isMoving);
            }
            
            //Reset jump
            if (isJumping && !player.GetComponent<PlayerController>().isJumping)
            {
                isJumping = false;
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
        else
        {

        }
    }

}
