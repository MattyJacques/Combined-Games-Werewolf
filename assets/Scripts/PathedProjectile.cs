using UnityEngine;
using System.Collections;

public class PathedProjectile : MonoBehaviour {

  private Transform destination;   //destination of shot
  private float speed;             //speed of shot
  private int damage = 15;         //damage to deal

  //initalise the shot
  public void Initialise(Transform newDestination, float newSpeed)
  {
    destination = newDestination; //set destination
    speed = newSpeed;             //set speed
  }
	
	// Update is called once per frame
	void Update ()
  {
    //move towards destination
    transform.position = Vector3.MoveTowards(transform.position,
                                             destination.position,
                                             Time.deltaTime * speed);
    //get distance squared
    var distanceSquared = (destination.transform.position - transform.position).sqrMagnitude;

    //i distance squared above number, return
    if (distanceSquared > 0.01f * 0.01f)
    {
      return;
    }
    //if hit destroy object
    Destroy(gameObject);
	}
  
  void OnTriggerEnter2D(Collider2D coll)
  { // If collided with player or enemy, do damage

    if (coll.CompareTag("Player") || coll.CompareTag("Enemy"))
    {
      coll.SendMessage("TakeDamage", damage);
      //deal damage and destroy object
      Destroy(this.gameObject);
    }

  } // OnTriggerEnter2D()
}
