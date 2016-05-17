using UnityEngine;
using System.Collections;

public class EnemyDino : Enemy
{ // Controller for the Dino type enemy

  private Animator theAnimator;        // Animation controller

  //private bool isWalking = false;      // Is the enemy walking

  private float attackTime = 0;         // Time until next attack

  void Awake ()
  { // Get the animator component on object creation

    theAnimator = GetComponent<Animator>();
    attackTime = 0;

	} // Start()
	
	void Update ()
  { // Check if the player is close enough to attack

    if (health > 0)
    {
        if (player != null && !player.GetComponent<PlayerController>().isHide)
        {
            if (Vector2.Distance(transform.position, player.transform.position)
          < rangeCheck && (attackTime + 3f < Time.time))
            { // If player is close enough to attack, attack

                theAnimator.SetTrigger("IsAttack");
                attackTime = Time.time + 1f;
            }
      }
    }
    else
    {
      StartCoroutine(Die());
    }

	} // Update()

    void OnCollisionEnter2D(Collision2D coll)
    {
        //If we attack and hit the player
        if (coll.gameObject.tag == "Player")
        {
            //Damage the player
            coll.gameObject.SendMessage("TakeDamage", damage);
        }
    }
} // DinoController
