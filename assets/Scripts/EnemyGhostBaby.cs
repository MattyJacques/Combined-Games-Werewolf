using UnityEngine;
using System.Collections;

public class EnemyGhostBaby : Enemy
{
  public bool facingRight = true;
  public bool inTrigger = false;
  //private bool isMoving = false;
  //private bool isAttack = false;
  private bool isDead = false;
  //private bool isHit = false;

  private Animator anim;

  // Use this for initialization
  void Start ()
  {
	  anim = GetComponent<Animator>();
  }
	
	// Update is called once per frame
	void Update ()
  {
	
    if(health > 0)
    {

    }
    else
    {
      Die();
      Destroy(this.gameObject);
    }

    if(inTrigger)
    {
      //do movement shit
    }

	}

  void OnTriggerEnter2D()
  {
    inTrigger = true;
  }

  void OnTriggerExit2D()
  {
    inTrigger = false;
  }

  void Die()
  {
    isDead = true;
    anim.SetBool("IsDead", isDead);
  }

  void Standard()
  {
    anim.SetBool("IsDead", isDead);
  }


  void Flip()
  {

    facingRight = !facingRight;

    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
  }
}
