using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour 
{

    public int numCoins = 10;
    public GameObject coinPrefab;
    public float offSetRange = 1.5f;

	// Use this for initialization
	void Start () 
    {

        SpawnCoins();

	}

    public void SpawnCoins()
    {
        for (int i = 0; i < numCoins; i++)
        {
            Vector2 spawnOffset = new Vector2(Random.Range(-offSetRange, offSetRange), Random.Range(-offSetRange, offSetRange));
            Instantiate(coinPrefab, (Vector2)transform.position + spawnOffset, Quaternion.identity);
        }
    }
}
