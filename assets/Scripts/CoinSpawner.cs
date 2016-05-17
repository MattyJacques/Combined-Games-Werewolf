using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour 
{

    public int numCoins = 10;       //num of coins to spawn
    public GameObject coinPrefab;   // prefab of coin to spawn
    public float offSetRange = 1.5f; //range offset for spawning of coin

	// Use this for initialization
	void Start () 
    {
        //on start spawn cins
        SpawnCoins();

	}

    public void SpawnCoins()
    {
        //for loop for coin spawning, spawns desired number of coins
        for (int i = 0; i < numCoins; i++)
        {
            //offst for coin spawning  = random range from offset
            Vector2 spawnOffset = new Vector2(Random.Range(-offSetRange, offSetRange), Random.Range(-offSetRange/2, offSetRange/2));
            //instantiate the coin at desired location
            Instantiate(coinPrefab, (Vector2)transform.position + spawnOffset, Quaternion.identity);
        }
    }
}
