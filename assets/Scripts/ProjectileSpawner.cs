using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {

  public Transform destination;
  public PathedProjectile projectile;

  public float speed;
  public float fireRate;

  private float nextShotSeconds;

  

	// Use this for initialization
	public void Start ()
  {
    nextShotSeconds = fireRate;
	}
	
	// Update is called once per frame
	void Update ()
  {
	  if((nextShotSeconds -= Time.deltaTime) > 0)
    {
      return;
    }
    nextShotSeconds = fireRate;
    var newProjectile = (PathedProjectile)Instantiate(projectile, 
                                                   transform.position,
                                                   transform.rotation);
    newProjectile.Initialise(destination, speed);
	}

  void OnDrawGizmos()
  {
    if(destination == null)
    {
      return;
    }
    Gizmos.color = Color.red;
    Gizmos.DrawLine(transform.position, destination.position);
  }
}
