using UnityEngine;
using System.Collections;

public class PathedProjectile : MonoBehaviour {

  private Transform destination;
  private float speed;
  private int damage = 40;

  public void Initialise(Transform newDestination, float newSpeed)
  {
    destination = newDestination;
    speed = newSpeed;
  }
	
	// Update is called once per frame
	void Update ()
  {
    transform.position = Vector3.MoveTowards(transform.position,
                                             destination.position,
                                             Time.deltaTime * speed);

    var distanceSquared = (destination.transform.position - transform.position).sqrMagnitude;

    if (distanceSquared > 0.01f * 0.01f)
    {
      return;
    }

    Destroy(gameObject);
	}
  
  void OnTriggerEnter2D(Collider2D coll)
  { // If collided with player, do damage

    if (coll.CompareTag("Player") || coll.CompareTag("Enemy"))
    {
      coll.SendMessage("TakeDamage", damage);
      Destroy(this.gameObject);
    }

  } // OnTriggerEnter2D()
}
