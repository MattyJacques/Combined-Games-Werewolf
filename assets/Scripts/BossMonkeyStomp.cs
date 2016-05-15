using UnityEngine;
using System.Collections;

public class BossMonkeyStomp : MonoBehaviour
{ 
  void OnTriggerEnter2D(Collider2D coll)
  {
    if (coll.CompareTag("Player"))
    {
      coll.SendMessage("TakeDamage", 40);
      GetComponent<BoxCollider2D>().enabled = false;
    }
  } // OnTriggerEnter2D()

  
  void Destroy()
  { // Destroy the object after animation

    Destroy(this.gameObject);     // Destroy the object

  } // Destroy()

}
