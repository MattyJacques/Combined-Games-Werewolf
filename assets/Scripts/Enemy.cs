using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int health;
    public bool scary;
    public float speed = 15;
    public int damage = 10;
    public int rangeCheck = 20;
    public GameObject player;

	void Start ()
    {
        health = 100;
        scary = false;

        player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () 
    {
	
	}

    public virtual void Move()
    {

    }
}
