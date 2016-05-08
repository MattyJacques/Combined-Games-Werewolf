using UnityEngine;
using System.Collections;

public class EnemyDino : Enemy
{ // Controller for the Dino type enemy

  private Animator theAnimator;        // Animation controller

  //private bool isWalking = false;      // Is the enemy walking
  private bool isDead = false;         // Is the enemy dead
  //private bool isAttacking = false;    // Is the enemy attacking

  private float attackTime = 0;         // Time until next attack

  void Awake ()
  { // Get the animator component on object creation

    theAnimator = GetComponent<Animator>();
    attackTime = 0;

	} // Start()
	
	void Update ()
  { // Check if the player is close enough to attack

    if (player != null)
    {
      if (Vector2.Distance(transform.position, player.transform.position) 
          < rangeCheck && (attackTime + 3f < Time.time))
      { // If player is close enough to attack, attack

        theAnimator.SetTrigger("IsAttack");
        attackTime = Time.time + 1f;
      }
    }

    if (health <= 0)
    {
      StartCoroutine(Die());
    }

	} // Update()

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

    theAnimator.SetBool("IsDead", true);     // Play dead animation

    // Wait until animation is finished to destroy enemy
    yield return new WaitForSeconds(theAnimator.GetCurrentAnimatorStateInfo(0).
                                    length);

    Destroy(this.gameObject);                             // Destroy enemy

  } // Die()

} // DinoController
