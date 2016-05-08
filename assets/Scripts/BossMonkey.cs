using UnityEngine;
using System.Collections;

public class BossMonkey : Enemy
{
  private Animator theAnimator;        // Animation controller

  private bool isIdle = true;          // Is the enemy idle
  private bool isDead = false;         // Is the enemy dead
  private bool isAttacking = false;    // Is the enemy attacking
  private bool hasTransformed = false; // If the enemy has transformed

  private float attackTime = 0;        // Time until next attack

  void Awake ()
  { // Get the animation controler

    theAnimator = GetComponent<Animator>();     // Get animation controller

	} // Awake()
	

	void Update ()
  {
    if (Vector2.Distance(transform.position, player.transform.position) < 
        rangeCheck && !hasTransformed)
    {
      Transform();
    }

    if (isAttacking && (Time.time > attackTime))
    {
      isAttacking = false;                              // Finishing attacking
      isIdle = true;
      theAnimator.SetBool("IsIdle", isIdle);
      theAnimator.SetBool("IsAttacking", isAttacking);
    }
  }

  void Transform()
  { // Change the enemy from the baby monkey to the actual boss monkey.

    theAnimator.Play("MonkeyTransform");            // Switch to transform state
    hasTransformed = true;

  } // Transform()

}
