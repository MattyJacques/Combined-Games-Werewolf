using UnityEngine;
using System.Collections;

public class TutorialMoon : MonoBehaviour
{

  private float timer;
	
	void Update ()
  { // Toggles the moon every 5 seconds

    timer -= Time.deltaTime;

    if (timer <= 0)
    {
      timer = 5;

      GetComponent<CircleCollider2D>().enabled = 
        !GetComponent<CircleCollider2D>().enabled;

      GetComponent<SpriteRenderer>().enabled =
        !GetComponent<SpriteRenderer>().enabled;
    }

	} // Update()
}
