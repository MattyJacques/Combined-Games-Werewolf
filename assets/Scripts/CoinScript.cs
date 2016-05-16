using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

    public float spawnTime; // time after spawn

	// Use this for initialization
	void Awake () 
    {
      this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
      spawnTime = 1.0f;
   //   StartCoroutine(ActivateCoin());
	}

    IEnumerator ActivateCoin()
    {
        yield return new WaitForSeconds(spawnTime);


    }

}
