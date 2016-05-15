using UnityEngine;
using System.Collections;

public class FollowEnemy : MonoBehaviour {

  public Vector3 offset;      // The offset at which the Health Bar follows the player.

  public Transform enemy;   // Reference to the enemy


  void Awake()
  {
    // Setting up the reference.
  //  enemy = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update()
  {
    // Set the position to the player's position with the offset.
    //transform.position = enemy.position + offset;
  }
}
