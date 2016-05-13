using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

  public int health;
  public bool scary;
  public float speed = 15;
  public int damage = 10;
  public int rangeCheck = 20;
  public GameObject player;

	void Start ()
  {
    health = 100;
    scary = false;

    player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () 
  {
	  if(health <= 0)
    {
      Die();                           //when health is < 0 DIe
    }

	}


 // public virtual void Attack()
//  {

//  }

  public virtual void Move()
  {

  }

  //run the animator and then destory the object
  public IEnumerator Die()
  { // Play animation then destroy the enemy when has no health left

    GetComponent<Animator>().SetBool("IsDead", true);     // Play dead animation

    // Wait until animation is finished to destroy enemy
    yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).
                                    length);

    Destroy(this.gameObject);                             // Destroy enemy

  } // Die()

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            //Physics2D.IgnoreCollison
        }
    }
}
