using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

    private float spawnTime; // time after spawn

	// Use this for initialization
	void Start () 
    {
      spawnTime = 2.0f;
      StartCoroutine(ActivateCoin());
	}

    IEnumerator ActivateCoin()
    {
        yield return new WaitForSeconds(spawnTime);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

    }

}
