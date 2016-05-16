using UnityEngine;
using System.Collections;

public class TutorialZone : MonoBehaviour
{
  [SerializeField]
  private int trigIndex;                    // Index of trigger for zone
  private bool isTriggered = false;         // Has the zone been triggered

	void OnTriggerEnter2D(Collider2D other)
  { // Send the trig index and the collider to the parent for handling of which
    // zone the player entered

    if (other.CompareTag("Player") && !isTriggered)
    { // Call to handle trigger event if player has entered
      isTriggered = true;
      GetComponentInParent<TutorialControl>().HandleTrigger(trigIndex, other);
    }

  } // OnTriggerEnter2D()

} // TutorialZone
