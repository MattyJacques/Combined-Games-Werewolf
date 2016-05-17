using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {

  public Transform destination;  //end loc of projectile
  public PathedProjectile projectile; //projectile to shoot
     
  public float speed;            //speed of projectile
  public float fireRate;         // fire rate of projectile

  private float nextShotSeconds; //next shot in seconds

  

	// Use this for initialization
	public void Start ()
  {
    nextShotSeconds = fireRate; //next shot time = fire rate
	}
	
	// Update is called once per frame
	void Update ()
  {
	  if((nextShotSeconds -= Time.deltaTime) > 0) //not time to shoot do nothing
    {
      return;
    }

    nextShotSeconds = fireRate;                 // reset time

    //create a new projectile
    var newProjectile = (PathedProjectile)Instantiate(projectile, 
                                                   transform.position,
                                                   transform.rotation);
    newProjectile.Initialise(destination, speed); //shoot a projectile
	}

  void OnDrawGizmos()
  {
    if(destination == null) //if no destination do nothing
    {
      return;
    }

    Gizmos.color = Color.red; //set line to red
    Gizmos.DrawLine(transform.position, destination.position); //draw shot line
  }
}
