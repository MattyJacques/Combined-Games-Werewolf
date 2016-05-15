using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

  public int health;
  public bool scary;
  public float speed = 15;
  public int damage = 10;
  public int rangeCheck = 20;
  public GameObject player;

  public SpriteRenderer healthBar;     // Reference to the sprite renderer of the health bar.
  private Vector3 healthScale;				// The local scale of the health bar initially (with full health).

  void Start ()
  {
    health = 100;
    scary = false;

  //  healthBar = GetComponentInChildren<SpriteRenderer>(); 

    player = GameObject.FindGameObjectWithTag("Player");
    healthScale = healthBar.transform.localScale;
  }


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


  public void TakeDamage(int damage)
  { // Subtract damage given as parameter from the current enemy health

    health -= damage;              // Subtract damage from health

    if (health <= 0)
    { // If health is equal or less than 0, kill the player
      StartCoroutine(Die());       // Call to kill player
    }

  } // TakeDamage()


  void OnTriggerEnter2D(Collider2D other)
  {
      if (other.gameObject.tag == "Instant Kill")
      {
          StartCoroutine(Die());
      }
  }
  
  public void UpdateHealthBar()
  {
    // Set the health bar's colour to proportion of the way between green and red based on the player's health.
    healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

    // Set the scale of the health bar to be proportional to the player's health.
    healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);

  }

}
