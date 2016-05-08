using UnityEngine;
using System.Collections;

public class BossMonkey : Enemy
{
  private Animator theAnimator;        // Animation controller

  private bool isDead = false;         // Is the enemy dead
  private bool hasTransformed = false; // If the enemy has transformed

  private float attackTime = 0;        // Time until next attack

  void Awake ()
  { // Get the animation controler

    theAnimator = GetComponent<Animator>();     // Get animation controller

	} // Awake()
	

	void Update ()
  {
    if (Vector2.Distance(transform.position, 
        player.transform.position) < rangeCheck)
    {
      if (!hasTransformed)
      {
        Transform();
      }
      else if (Time.time > attackTime)
      {
        attackTime = Time.time + 2.5f;
        Attack();
      }
    }

  } // Update()

  void Transform()
  { // Change the enemy from the baby monkey to the actual boss monkey.

    hasTransformed = true;                              // Set transform to true
    theAnimator.SetBool("IsTransform", hasTransformed); // Start transfrorm

  } // Transform()


  void Attack()
  { // Chooses a random attack from the spit or stomp attacks

    int randAttack = Random.Range(0, 2);

    if (randAttack == 0)
    {
      theAnimator.SetTrigger("IsStomp");
    }
    else if (randAttack == 1)
    {
      theAnimator.SetTrigger("IsSpit");
    }

  } // Attack()

}
