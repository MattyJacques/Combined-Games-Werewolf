using UnityEngine;
using System.Collections;

public class TutorialControl : MonoBehaviour
{

  private GameObject moon;           // Reference to moon
  private GameObject player;         // Reference to player
  public GameObject[] wizards;       // Pool of tutorial wizards
	

  void Start()
  { // Disable the sprites of wizards so the player can't see them until trigger

    player = GameObject.FindGameObjectWithTag("Player");      // Get player
    moon = GameObject.FindGameObjectWithTag("Moon");          // Get moon

    foreach (GameObject wizard in wizards)
    { // For each wizard, disable, sprite, speech bubble and speech text
      wizard.GetComponent<SpriteRenderer>().enabled = false;       // Sprite
      wizard.transform.GetChild(0).
        GetComponentInChildren<SpriteRenderer>().enabled = false;  // Bubble
      wizard.transform.GetChild(0).GetChild(0).
        GetComponentInChildren<MeshRenderer>().enabled = false;    // Text
    }

  } // Start()


	void Update ()
  { // Updates moon position to the players unless over limit
      if (player != null)
      { //Check player is alive
          if (player.transform.position.x < 28)
          { // If player x is less than 28, update moon position
              moon.transform.position = 
                  new Vector3(player.transform.position.x - 4,
                              player.transform.position.y + 4,
                              player.transform.position.z);
          }
      }

	} // Update()


  public void HandleTrigger(int trigIndex, Collider2D other)
  { // Handle the trigger from a child, checking which zone the child trigger
    // entered

    // Enable sprite
    wizards[trigIndex - 1].GetComponent<SpriteRenderer>().enabled = true;

    // Start appear animation
    wizards[trigIndex - 1].GetComponent<Animator>().SetTrigger("Appear");

    // Enable speech bubble sprite
    wizards[trigIndex - 1].transform.GetChild(0).
      GetComponentInChildren<SpriteRenderer>().enabled = true;

    // Enable text
    wizards[trigIndex - 1].transform.GetChild(0).GetChild(0).
      GetComponentInChildren<MeshRenderer>().enabled = true;

  } // HandleTrigger()

} // TutorialControl
