using UnityEngine;
using System.Collections;

public class BossMonkey : Enemy
{
  private Animator theAnimator;        // Animation controller

  private bool isIdle = true;          // Is the enemy idle
  private bool isDead = false;         // Is the enemy dead
  private bool isAttacking = false;    // Is the enemy attacking

  private float attackTime = 0;        // Time until next attack

  void Awake ()
  { // Get the animation controler

    theAnimator = GetComponent<Animator>();     // Get animation controller

	} // Awake()
	

	void Update ()
  {
    if (Vector2.Distance(transform.position, player.transform.position) < 
        rangeCheck && !isAttacking)
    { // If player is close enough to attack, attack
      isIdle = false;
      theAnimator.SetBool("IsIdle", isIdle);
      isAttacking = true;                               // Set attacking to true
      theAnimator.SetBool("IsAttacking", isAttacking);  // Play attack animation
      attackTime = Time.time + 1f;
      //Attack();
    }

    if (isAttacking && (Time.time > attackTime))
    {
      isAttacking = false;                              // Finishing attacking
      isIdle = true;
      theAnimator.SetBool("IsIdle", isIdle);
      theAnimator.SetBool("IsAttacking", isAttacking);
    }
  }

}
