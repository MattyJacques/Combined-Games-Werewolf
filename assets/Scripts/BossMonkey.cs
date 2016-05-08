using UnityEngine;
using System.Collections;

public class BossMonkey : Enemy
{
  private Animator theAnimator;        // Animation controller

  private bool hasTransformed = false; // If the enemy has transformed

  private float attackTime = 0;        // Time until next attack

  void Awake ()
  { // Get the animation controler

    health = 1000;
    theAnimator = GetComponent<Animator>();     // Get animation controller

	} // Awake()
	

	void Update ()
  {
    if (health > 0 && player != null)
    {
      if (Vector2.Distance(transform.position,
          player.transform.position) < rangeCheck)
      {
        if (!hasTransformed && health < 1000)
        {
          Transform();
        }
        else if (Time.time > attackTime)
        {
          attackTime = Time.time + 2.5f;
          Attack();
        }
      }
    }
    else if (health <= 0)
    {
      StartCoroutine(Die());
    }

  } // Update()

  void Transform()
  { // Change the enemy from the baby monkey to the actual boss monkey.

    hasTransformed = true;                              // Set transform to true
    theAnimator.SetBool("IsTransform", hasTransformed); // Start transfrorm

  } // Transform()


  override public void Attack()
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

  IEnumerator Die()
  { // Play animation then destroy the enemy when has no health left

    theAnimator.SetBool("IsDead", true);     // Play dead animation

    // Wait until animation is finished to destroy enemy
    yield return new WaitForSeconds(theAnimator.GetCurrentAnimatorStateInfo(0).
                                    length);

    Destroy(this.gameObject);                             // Destroy enemy

  } // Die()

}
