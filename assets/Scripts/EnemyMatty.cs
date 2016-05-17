using UnityEngine;
using System.Collections;

public class EnemyMatty : Enemy
{
    //Referemce tp animator
    private Animator anim;
    //Reference to 
    private Rigidbody2D rb;

    //Is hit check
    private bool isHit;
    //Is moving check
    private bool isMoving;
    //IS jumping check
    private bool isJumping;

    void Awake()
    {
        //Set is hit to false
        isHit = false;
        //Set is moving to false
        isMoving = false;
        //Set is juming to false
        isJumping = false;
        //Set rigidbody
        rb = GetComponent<Rigidbody2D>();
        //Set animator
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        //If alive 
        if (health > 0)
        {
            //If there is a plater and they are not hiding
            if (player != null &&
                !player.GetComponent<PlayerController>().isHide)
            {
                //If in range
                if (Vector2.Distance(player.transform.position,
                    transform.position) < 2)
                {
                    //Stand still
                    isMoving = false;
                    //Set animation
                    anim.SetBool("IsMoving", isMoving);

                    //Jump if the player does
                    if (player.GetComponent<PlayerController>().isJumping &&
                        !isJumping)
                    {
                        //Copy the jump by adding a jump force
                        rb.AddForce(new Vector2(0f,
                            player.GetComponent<PlayerController>().jumpForce));
                        //Set jumping to true
                        isJumping = true;
                    }
                }
                //If within range
                else if (Vector2.Distance(player.transform.position,
                    transform.position) < rangeCheck)
                {
                    //Move closer to the player
                    rb.AddForce((player.transform.position -
                        transform.position) * speed * Time.deltaTime);
                    //Set moving
                    isMoving = true;
                    //Set animation
                    anim.SetBool("IsMoving", isMoving);
                }
                else //Out of range
                {
                    //Stop moving
                    isMoving = false;
                    //Set animation
                    anim.SetBool("IsMoving", isMoving);
                }

                //Reset jump
                if (isJumping &&
                    !player.GetComponent<PlayerController>().isJumping)
                {
                    //Set jumping to false
                    isJumping = false;
                }

                //Look direction
                if ((player.transform.position.x < transform.position.x) &&
                    (transform.localScale.x < 0))
                {
                    //Set the sprite direction
                    transform.localScale = new Vector3(transform.localScale.x *
                        -1, transform.localScale.y, transform.localScale.z);
                }
                else if ((player.transform.position.x > transform.position.x)
                    && (transform.localScale.x > 0))
                {
                    //Set the sprite direction
                    transform.localScale = new Vector3(transform.localScale.x *
                        -1, transform.localScale.y, transform.localScale.z);
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
