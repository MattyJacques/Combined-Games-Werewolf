using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

    public float spawnTime; // time after spawn

	// Use this for initialization
	void Awake () 
    {
      //set trigger to true 
      this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        //set spawn time
      spawnTime = 1.0f;
   //   StartCoroutine(ActivateCoin());
	}

    IEnumerator ActivateCoin()
    {
        //activate coin after period of time
        yield return new WaitForSeconds(spawnTime);


    }

}
