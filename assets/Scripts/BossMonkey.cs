using UnityEngine;
using System.Collections;

public class BossMonkey : Enemy
{
  // Component References
  private Animator theAnimator;        // Animation controller
  public GameObject endWall;           // Wall blocking end level

  // Attack Information
  private float attackTime = 0;        // Time until next attack
  public GameObject projectile;        // Spit projectile prefab
  public GameObject stompAttack;       // Stomp attack prefab
  private Vector3 stompPos;            // Position of stomp attack

  // Transform Information
  private bool hasTransformed = false; // If the enemy has transformed
  private float transformTime;         // Holds time until transform complete

  void Awake ()
  { // Get the animation controler
    theAnimator = GetComponent<Animator>();     // Get animation controller

	} // Awake()

  void Start()
  { // Override Enemy Start() and set health

    health = 1000;
    GameObject moon = GameObject.FindGameObjectWithTag("Moon");
    moon.GetComponent<MoonController>().endPos = new Vector3(30f, 4.5f, 80f);
    player = GameObject.FindGameObjectWithTag("Player");

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


  IEnumerator TriggerEndLevel()
  {
    yield return new WaitForSeconds(4f);

    player.SendMessage("EndLevel");
    Destroy(endWall);

  } // TriggerEndLevel()


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


  void SetStompPosition()
  { // Store the players current position so the stomp attack will be created
    // where the player was located when animation starts
      if (player != null)
      {
          stompPos = player.transform.position;       // Store player position
          stompPos.y -= 0.8f;                          // Move position down
      }

  } // SetStompPosition()

  
  void CreateStomp()
  { // Create the stomp attack prefab in the stored location from earlier in
    // the animation

    Instantiate(stompAttack, stompPos, transform.rotation); // Create stomp obj

  } // CreateStomp()

}
