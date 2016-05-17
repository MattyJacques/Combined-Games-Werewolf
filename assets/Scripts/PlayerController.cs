using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

  // Player Information
  public bool facingRight = true;          // Is player facing right
  private bool canClimb = false;           // Is in front of climbable object
  private bool canHide = false;            // Is player in front of hiding spot
  private float moveSpeed = 2.5f;          // Move speed of player
  public float jumpForce = 1000f;          // The jump force of player
  public int health;                       // Current health of player

  // Ground check Info
  public Transform ground;                 // Position of player feet
  private bool isGrounded = false;         // Is the player currently grounded

  // Level Info
  public bool endLevel = false;            // Is the end level activated  

  // Player Component References
  private Animator anim;                   // Animation controller
  private Rigidbody2D rb;                  // Rigidbody of player
  public BoxCollider2D trigger;            // Holds the player attack trigger
  public GameObject gameOverMenu;          // Gamer over menu
  private GameObject gameController;       // Game Controller for coin collect

  //Audio Component Setup
  private AudioSource playerAudio;         //Audio source
  public AudioClip coinPickup;             //Coin pickup sound
  public AudioClip swipeSound;             //Swipe attack sound
  public GameObject deathSound;            //Death sound

  // Animation bools
  private bool isWolf = true;              // Is the player a wolf
  private bool isWalking = false;          // Is the player walking
  private bool isAttacking = false;        // Is the player attacking
  private bool isClimb = false;            // Is the player climbing
  [HideInInspector]
  public bool isHide = false;              // Is the player hiding
  [HideInInspector]
  public bool isJumping = false;           // Is the player jumping


  void Awake()
  { // Get the animation controller and rigidbody on object creation, disable 
    // the attack trigger

    anim = GetComponent<Animator>();       // Get animation controller
    rb = GetComponent<Rigidbody2D>();      // Get rigidbody 

    // Disable hiding collider
    GetComponent<EdgeCollider2D>().enabled = false;

    // Get the game controller so the player can collect coins
    gameController = GameObject.FindGameObjectWithTag("GameController");

    trigger.enabled = false;                // Disable attack trigger
    health = GameSaves.saves.maxHealth;     // Set max health

        playerAudio = GetComponent<AudioSource>();

  } // Awake()


  void Update()
  { // Checks if end level is activated, if so, moves player to right. Checks if
    // the player is grounded, check for user input for climb, hiding and attack

    if (!endLevel)
    { // If end level is not activated, check for the gorund and check for any
      // user input

      // Check if the player is touching ground
      isGrounded = Physics2D.Linecast(transform.position, ground.position, 1
                   << LayerMask.NameToLayer("Ground"));

      if (Input.GetButtonDown("Jump") && isGrounded && !isHide)
      { // If grounded and not hiding while player is inputting jump, start jump

        isJumping = true;                       // Set jump to true
        anim.SetBool("IsJumping", isJumping);   // Set animation bool

        if (!isWolf)
        { // If human, jump with half the jumpForce
          GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce / 2f));
        }
        else
        { // If wolf, jump at full jumpForce
          GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
      }

      if (Input.GetButtonDown("Fire1") && !isJumping && !isHide && !isClimb)
      { // If Fire1 input, and not jumping, hiding or climbing, start attack

        playerAudio.clip = swipeSound;
        playerAudio.Play();
        isAttacking = true;                          // Set attacking to true
        anim.SetBool("IsAttacking", isAttacking);    // Set animation bool
        trigger.enabled = true;                      // Enable attack trigger

      }

      if (Input.GetButtonDown("Fire2") && !isHide && canHide && !isWolf)
      { // If Fire2 input while not hiding, while player can hide and current
        // not wolf, start hide process

        isHide = true;                               // Set hide bool
        anim.SetBool("IsHide", isHide);              // Set animation bool
        Hiding();                                    // Call to process hide
      }
      else if (Input.GetButtonDown("Fire2") && isHide)
      { // else if Fire2 input while already hiding, stop hiding

        isHide = false;                              // Set hide to false
        anim.SetBool("IsHide", isHide);              // Set animation bool
        Hiding();                                    // Call to process hide
      }
    }
    else
    { // Else if end level activated, move player to right
      transform.position = new Vector3(transform.position.x + (Time.deltaTime *
                                      moveSpeed), transform.position.y,
                                      transform.position.z);
    }

  } // Update()


  void FixedUpdate()
  { // Check animation bools to change player position. also check for player
    // movement input. Sprite face direction correction

    if (isJumping)
    { // If jumping check if grounded to end jumping animation

      if (isGrounded && rb.velocity.y < 0)
      { // If grounded and y velocity is negative, call to end jumping 
        // animation
          Jumping();
      }
    }

    if (!isAttacking && !endLevel)
    { // If not attacking and not end level, check for movement input

      float h = Input.GetAxis("Horizontal");       // Get x input
      float v = Input.GetAxis("Vertical");         // Get y input

      if (v != 0 && !isWolf && canClimb && !isHide)
      { // If some vertical input and not wolf, can climb and not currently
        // hiding then start climbing

        isClimb = true;                 // Set climb bool to true
        rb.gravityScale = 0;            // Set gravity to 0

        // Move player upwards
        transform.position = new Vector3(transform.position.x,
                                          transform.position.y +
                                          (v * Time.deltaTime * moveSpeed),
                                          transform.position.z);
      }
      else
      { // Else stop climbing and process x input

        isClimb = false;           // Set climbing to false
        rb.gravityScale = 1;       // Reset gravity

        if (h != 0 && !isHide)
        { // If horizontal is not 0, and not currently hiding, start walking

          isWalking = true;        // Set walking to true

          // Move player in walking direction
          transform.position = new Vector3(transform.position.x +
                                            (h * Time.deltaTime * moveSpeed),
                                            transform.position.y,
                                            transform.position.z);
        }
        else
        { // Else stop walking
          isWalking = false;
        }
      } // if (v != 0 && !isWolf && canClimb && !isHide)

      anim.SetBool("IsClimb", isClimb);       // Set climbing bool
      anim.SetBool("IsWalking", isWalking);   // Set walking bool

      if (h > 0 && !facingRight && !isHide)
      { // If not facing right but should be, flip
        Flip();
      }
      else if (h < 0 && facingRight && !isHide)
      { // If facing right but shouldn't be, flip
        Flip();
      }
    }

  } // FixedUpdate()


  void OnCollisionEnter2D(Collision2D other)
  { // When player jumps on moving platform, make the platform a parent of the
    // player for automatic movement adjusting

    if (other.gameObject.tag == "Platform")
    { // If colliding with platform, set parent to platform
      transform.parent = other.transform;
    }

  } // OnCollisionEnter2D()


  void OnCollisionExit2D(Collision2D other)
  { // When player jumps on moving platform, make the platform a parent of the
    // player for automatic movement adjusting

    if (other.gameObject.tag == "Platform")
    { // If colliding with platform, set parent to platform
      transform.parent = null;
    }

  } // OnCollisionExit2D()

  void OnTriggerEnter2D(Collider2D other)
  { // On trigger enter check the tag of object and process tag actions

    if (other.gameObject.tag == "Moon" && !isWolf)
    { // If trigger was moon, transform into wolf

      anim.Play("Transform");                       // Start transform animation
      isWolf = true;                                // Set wolf bool
      StartCoroutine(ChangeLayerWeight(0f));        // Change to wolf animation
    }
    else if (other.gameObject.tag == "Instant Kill")
    { // If triggered by hazard, take 1000 damage
      TakeDamage(1000);
    }
    else if (other.gameObject.tag == "Coin")
    { // If collided with coin, call to collect coin
      playerAudio.clip = coinPickup;
      playerAudio.Play();
      gameController.SendMessage("AddCoin", other.gameObject);
    }
    else if (other.gameObject.tag == "Climb")
    { // If object is a climbalbe object, set climbable to true
      canClimb = true;
    }
    else if (other.gameObject.tag == "Hide")
    { // If can hide behind object, update bool
      canHide = true;
    }
  } // OnTriggerEnter2D()


  void OnTriggerExit2D(Collider2D other)
  { // On trigger exit check the tag of object and process tag actions

    if (other.gameObject.tag == "Moon" && isWolf)
    { // If trigger was moon, transform into human

      anim.Play("Transform");                       // Start transform animation
      isWolf = false;                               // Set wolf to false
      StartCoroutine(ChangeLayerWeight(1f));        // Change to human animation

    }
    else if (other.gameObject.tag == "Climb")
    { // If object is a climbalbe object, set climbable to false
      canClimb = false;
    }
    else if (other.gameObject.tag == "Hide")
    { // If can  leaving hide spot object, update bool
      canHide = false;
    }

  } // OnTriggerExit2D()


  IEnumerator ChangeLayerWeight(float weight)
  { // Change animation layer weight to parameter after half a second

    yield return new WaitForSeconds(0.5f);       // Wait half a second
    anim.SetLayerWeight(1, weight);              // Set new weight for layer

  } // ChangeLayerWeight()


  void Flip()
  { // Flip the sprite to the opposite direction

      facingRight = !facingRight;               // False opposite direction

      Vector3 theScale = transform.localScale;  // Get scale
      theScale.x *= -1;                         // Toggle negative
      transform.localScale = theScale;          // Set scale

  } // Flip()


  void Attacking()
  { // Stop player attacking

      isAttacking = false;                          // Set attacking to false
      anim.SetBool("IsAttacking", isAttacking);     // Set animation bool
      trigger.enabled = false;                      // Deactive attack trigger

  } // Attack()


  void Jumping()
  { // Play the jump animation

      isJumping = false;                      // Set jumping to false
      anim.SetBool("IsJumping", isJumping);   // Set jump animation to false

  } // Jumping()


  void Hiding()
  { // Activates/Deactivates the player hiding depending on isHide

    GetComponent<BoxCollider2D>().enabled = !isHide;     // Toggle box colider
    GetComponent<CircleCollider2D>().enabled = !isHide;  // Toggle circle coll
    GetComponent<EdgeCollider2D>().enabled = isHide;     // Toggle edge coll

    if (isHide)
    { // If not hiding, hide
        transform.position = new Vector3(transform.position.x,
                                          transform.position.y, 0.1f);
    }
    else
    { // If hiding, unhide
        transform.position = new Vector3(transform.position.x,
                                          transform.position.y, -0.1f);
    }

  } // Hiding()


  void EndLevel()
  { // Starts the end level sequence

    anim.SetBool("IsWalking", true);          // Set walking animation
    endLevel = true;                          // Set end level

    // Start moving player to right
    transform.position = new Vector3(transform.position.x + (Time.deltaTime *
                                      moveSpeed), transform.position.y,
                                      transform.position.z);

  } // EndLevel()


  public void TakeDamage(int damage)
  { // Subtract damage given as parameter from the current player health

      health -= damage;              // Subtract damage from health

      if (health <= 0)
      { // If health is equal or less than 0, kill the player
        StartCoroutine(Die());       // Call to kill player

      }

  } // TakeDamage()


  IEnumerator Die()
  { // Play animation then destroy the player when has no health left

      rb.gravityScale = 0;              // Set graivty to 0
      rb.velocity = new Vector2(0, 0);  // Clear velocity
      anim.SetBool("IsDead", true);     // Play dead animation
        // Wait until animation is finished to destroy player
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).
                                      length);
        Instantiate(deathSound, transform.position, Quaternion.identity);
      Camera.main.transform.parent = null;         // Disconnect camera
      Destroy(this.gameObject);                    // Destroy player
      gameOverMenu.SetActive(true);                // Show game over

  } // Die()

}
