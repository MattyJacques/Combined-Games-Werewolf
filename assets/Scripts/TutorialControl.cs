using UnityEngine;
using System.Collections;

public class TutorialControl : MonoBehaviour
{

  private float timer;

  [SerializeField]
  private GameObject moon;
  public GameObject[] wizards;
	

  void Start()
  { // Disable the sprites of wizards so the player can't see them until trigger

    foreach (GameObject wizard in wizards)
    {
      wizard.GetComponent<SpriteRenderer>().enabled = false;
      wizard.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().enabled = false;
      wizard.transform.GetChild(0).GetChild(0).GetComponentInChildren<MeshRenderer>().enabled = false;
    }

  } // Start()


	void Update ()
  { // Toggles the moon every 5 seconds

    UpdateMoon();              // Update the moon timer and check for toggle



	} // Update()


  void UpdateMoon()
  { // Update the moon toggle timer, if timer is up toggle the mood into
    // opposite state

    timer -= Time.deltaTime;         // Take frametime away from timer

    if (timer <= 0)
    { // If timer is lower or equal to zero, toggle the moon

      timer = 5;                     // Reset timer

      // Toggle the circle collider of the moon to stop transforms
      moon.GetComponent<CircleCollider2D>().enabled =
        !moon.GetComponent<CircleCollider2D>().enabled;

      // Toggle the sprite renderer to hide the moon
      moon.GetComponent<SpriteRenderer>().enabled =
        !moon.GetComponent<SpriteRenderer>().enabled;
    }

  } // UpdateMoon()


  public void HandleTrigger(int trigIndex, Collider2D other)
  { // Handle the trigger from a child, checking which zone the child trigger
    // entered

    Debug.Log("Player has entered zone " + trigIndex);
    wizards[trigIndex - 1].GetComponent<SpriteRenderer>().enabled = true;
    wizards[trigIndex - 1].GetComponent<Animator>().SetTrigger("Appear");
    wizards[trigIndex - 1].transform.GetChild(0).
      GetComponentInChildren<SpriteRenderer>().enabled = true;
    wizards[trigIndex - 1].transform.GetChild(0).GetChild(0).
      GetComponentInChildren<MeshRenderer>().enabled = true;

  } // HandleTrigger()

} // TutorialControl
