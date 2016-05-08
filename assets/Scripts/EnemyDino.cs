using UnityEngine;
using System.Collections;

public class EnemyDino : Enemy
{ // Controller for the Dino type enemy

  private Animator theAnimator;        // Animation controller

  private bool isIdle = true;          // Is the enemy idle
  //private bool isWalking = false;      // Is the enemy walking
  private bool isDead = false;         // Is the enemy dead
  private bool isAttacking = false;    // Is the enemy attacking

  private float attackTime = 0;         // Time until next attack

  void Awake ()
  { // Get the animator component on object creation

    theAnimator = GetComponent<Animator>();
    theAnimator.SetBool("IsIdle", isIdle);
    attackTime = 0;

	} // Start()
	
	void Update ()
  { // Check if the player is close enough to attack

    if (Vector2.Distance(transform.position, player.transform.position) < rangeCheck &&
        !isAttacking)
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

	} // Update()

  IEnumerator Attack()
  { // Makes the enemy attack the player

    isAttacking = true;                               // Set attacking to true
    theAnimator.SetBool("IsAttacking", isAttacking);  // Play attack animation

    // Wait for animation to be over
    yield return new WaitForSeconds(theAnimator.GetCurrentAnimatorStateInfo(0).
                                    length);

    isAttacking = false;                              // Finishing attacking
    isIdle = true;
    theAnimator.SetBool("IsIdle", isIdle);
    theAnimator.SetBool("IsAttacking", isAttacking);

  } // Attack()

  void TakeDamage(int damage)
  { // Minus damage from health

    health -= damage;     // Subtract player damage from enemy health

    if (health <= 0)
    { // If health is equal or below 0, call ot kill enemy
      Die();
    }

  } // TakeDamage()

  IEnumerator Die()
  { // Play animation then destroy the enemy when has no health left

    theAnimator.SetBool("IsDead", isDead);     // Play dead animation

    // Wait until animation is finished to destroy enemy
    yield return new WaitForSeconds(theAnimator.GetCurrentAnimatorStateInfo(0).
                                    length);

    Destroy(this);                             // Destroy enemy

  } // Die()

} // DinoController
