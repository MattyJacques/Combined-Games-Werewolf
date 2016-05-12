using UnityEngine;
using System.Collections;

public class PathedProjectile : MonoBehaviour {

  private Transform destination;
  private float speed;

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
}
