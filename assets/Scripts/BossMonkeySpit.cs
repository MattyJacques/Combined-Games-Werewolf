using UnityEngine;
using System.Collections;

public class BossMonkeySpit : MonoBehaviour
{

  void Update()
  {
    Vector2 dir = new Vector2(-1, 0);

    GetComponent<Rigidbody2D>().AddForce(dir * 500 * Time.deltaTime);

    if (!GetComponent<SpriteRenderer>().isVisible)
    { // If projectile is offscreen, destroy object
      Destroy(this.gameObject);
    }

  } // Update()

	void OnTriggerEnter2D(Collider2D coll)
  {
    if (coll.gameObject.tag == "Enviroment")
    {
      Destroy(this.gameObject);
    }
    else if (coll.gameObject.tag == "Player")
    {
      coll.SendMessage("TakeDamage", 10);
      Destroy(this.gameObject);
    }
  } // OnTriggerEnter2D()
}
