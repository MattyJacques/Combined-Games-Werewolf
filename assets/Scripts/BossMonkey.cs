using UnityEngine;
using System.Collections;

public class BossMonkey : Enemy
{
  private Animator theAnimator;        // Animation controller
  public GameObject projectile;        // Spit projectile prefab

  private bool hasTransformed = false; // If the enemy has transformed
  private float transformTime;

  private float attackTime = 0;        // Time until next attack

  void Awake ()
  { // Get the animation controler

    theAnimator = GetComponent<Animator>();     // Get animation controller

	} // Awake()

  void Start()
  { // Override Enemy Start() and set health

    health = 1000;

  } // Start()
	

	void Update ()
  {
    if (health > 0 && player != null)
    {
      if (Vector2.Distance(transform.position,
          player.transform.position) < rangeCheck)
      {
        if (!hasTransformed && health < 1000)
        {
          Transform();
        }
        else if (Time.time > attackTime)
        {
          attackTime = Time.time + 2.5f;
          Attack();
        }
      }
    }
    else if (health <= 0)
    {
      StartCoroutine(Die());
    }

        if (Time.time < transformTime)
        {
            Vector2 spriteSize = GetComponent<SpriteRenderer>().sprite.bounds.size;
            gameObject.GetComponent<BoxCollider2D>().size = spriteSize;
            gameObject.GetComponent<BoxCollider2D>().offset = GetComponent<SpriteRenderer>().sprite.bounds.center;
        }

  } // Update()

  void Transform()
  { // Change the enemy from the baby monkey to the actual boss monkey.

    hasTransformed = true;                              // Set transform to true
    theAnimator.SetBool("IsTransform", hasTransformed); // Start transfrorm

    transformTime = Time.time + 9.16f;

  } // Transform()


  void Attack()
  { // Chooses a random attack from the spit or stomp attacks

    int randAttack = Random.Range(0, 2);

    if (randAttack == 0)
    {
      theAnimator.SetTrigger("IsStomp");
    }
    else if (randAttack == 1)
    {
      theAnimator.SetTrigger("IsSpit");
    }

  } // Attack()


  public void CreateSeed(int mode)
  { // Creates a seed using the projectile prefab, uses mode to check if seed
    // is top, middle or bottom

    if (mode == 1)
    {
      Vector2 pos = new Vector2(transform.position.x - 2.67f,
                                transform.position.y + 0.41f);

      GameObject spit = (GameObject)Instantiate(projectile, pos,
                                                transform.rotation);



      spit.transform.parent = this.gameObject.transform;
    }
    else if (mode == 2)
    {
      Vector2 pos = new Vector2(transform.position.x - 2.67f, 
                                transform.position.y + 0.81f);

      GameObject spit = (GameObject)Instantiate(projectile, pos, 
                                                transform.rotation);

      

      spit.transform.parent = this.gameObject.transform;
    }
    else if (mode == 3)
    {
      Vector2 pos = new Vector2(transform.position.x - 2.67f,
                                transform.position.y + 1.21f);

      GameObject spit = (GameObject)Instantiate(projectile, pos,
                                                transform.rotation);



      spit.transform.parent = this.gameObject.transform;
    }

  } // CreateSeed()

}
