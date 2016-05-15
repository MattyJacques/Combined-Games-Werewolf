using UnityEngine;
using System.Collections;

public class PlayerAttackTrig : MonoBehaviour {

  public int damage = 100;                  // How much damage player does

	void OnTriggerEnter2D(Collider2D col)
  { // Calls enemy function so enemy within range gets damaged

    if (col.CompareTag("Enemy"))
    { // If collider is from a enemy, call to take damage
      
      // INSERT DO DAMAGE CODE 
      col.SendMessage("TakeDamage", damage);
        //col.GetComponent<Enemy>().health -= damage;
    }

  } // OnTriggerEnter2D()

}
