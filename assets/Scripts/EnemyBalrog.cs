using UnityEngine;
using System.Collections;

public class EnemyBalrog : Enemy {

    private Animator anim;
    private Rigidbody2D rb;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
