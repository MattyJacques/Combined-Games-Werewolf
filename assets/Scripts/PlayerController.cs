using UnityEngine;
using UnityEditor.Animations;
using System.Collections;

public class PlayerController : MonoBehaviour
{

  // Player Information
	public bool facingRight = true;		       // Is player facing right
  private bool canClimb = false;           // Is in front of climbable object
  private bool canHide = false;            // Is player in front of hiding spot
  public float moveSpeed = 1.5f;				   // Move speed of player
	public float jumpForce = 1000f;          // The jump force of player
  public int health = 200;                 // Current health of player

  // Ground check Info
  public Transform ground;                 // Position of player feet
  private bool isGrounded = false;         // Is the player currently grounded

  // Level Info
  private bool endLevel = false;           // Is the end level activated  

  // Player Component References
	private Animator anim;                   // Animation controller
  private Rigidbody2D rb;                  // Rigidbody of player
  public BoxCollider2D trigger;            // Holds the player attack trigger
  public GameObject gameOverMenu;          // Gamer over menu
  private GameObject gameController;       // Game Controller for coin collect

  // Animation bools
	private bool isWolf = true;              // Is the player a wolf
  private bool isWalking = false;          // Is the player walking
  private bool isAttacking = false;        // Is the player attacking
  private bool isClimb = false;            // Is the player climbing
  [HideInInspector]
  public bool isHide = false;              // Is the player hiding
  [HideInInspector]
  public bool isJumping = false;           // Is the player jumping



  void Awake ()
	{ // Get the animation controller and rigidbody on object creation, disable 
    // the attack trigger

		anim = GetComponent<Animator> ();       // Get animation controller
    rb = GetComponent<Rigidbody2D>();       // Get rigidbody 

    GetComponent<EdgeCollider2D>().enabled = false;

    // Get the game controller so the player can collect coins
    gameController = GameObject.FindGameObjectWithTag("GameController");

    trigger.enabled = false;                // Disable attack trigger

	} // Awake()


	void Update ()
	{
    if (!endLevel)
    {
      isGrounded = Physics2D.Linecast(transform.position, ground.position, 1
                   << LayerMask.NameToLayer("Ground"));

      if (Input.GetButtonDown("Jump") && isGrounded && !isHide) //Add grounded "&& grounded"
      {
        isJumping = true;
        anim.SetBool("IsJumping", isJumping);
        if (!isWolf)
        {
          GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce / 2f));
        }
        else
        {
          GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
      }

      if (Input.GetButtonDown("Fire1") && !isJumping && !isHide)
      {
        isAttacking = true;
        anim.SetBool("IsAttacking", isAttacking);

        trigger.enabled = true;                      // Enable attack trigger
      }

      if (Input.GetButtonDown("Fire2") && !isHide && canHide && !isWolf)
      {
        isHide = true;
        anim.SetBool("IsHide", isHide);
        Hiding();
      }
      else if (Input.GetButtonDown("Fire2") && isHide)
      {
        isHide = false;
        anim.SetBool("IsHide", isHide);
        Hiding();
      }
    }
    else
    {
      transform.position = new Vector3(transform.position.x + (Time.deltaTime *
                                     moveSpeed), transform.position.y,
                                     transform.position.z);
    }

  } // Update()


	void FixedUpdate ()
	{
    if (isJumping)
    {
      if (isGrounded && rb.velocity.y < 0)
      { 
        Jumping();
      }
    }

    if (!isAttacking && !endLevel)
    {
      float h = Input.GetAxis("Horizontal");
      float v = Input.GetAxis("Vertical");

      if (v != 0 && !isWolf && canClimb && !isHide)
      {
        isClimb = true;
        rb.gravityScale = 0;
        transform.position = new Vector3(transform.position.x, 
                                         transform.position.y + 
                                         (v * Time.deltaTime * moveSpeed),
                                         transform.position.z);
      }
      else
      {
        isClimb = false;
        rb.gravityScale = 1;

        if (h != 0 && !isHide)
        {
          isWalking = true;

          transform.position = new Vector3(transform.position.x +
                                           (h * Time.deltaTime * moveSpeed),
                                           transform.position.y,
                                           transform.position.z);
        }
        else
        {
          isWalking = false;
        }
      }

      anim.SetBool("IsClimb", isClimb);

      anim.SetBool("IsWalking", isWalking);

      if (h > 0 && !facingRight && !isHide)
      {
        Flip();
      }
      else if (h < 0 && facingRight && !isHide)
      {
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

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Moon" && !isWolf) {
      anim.Play("Transform");
      isWolf = true;
      StartCoroutine(ChangeLayerWeight(0f));         // Change to wolf animation
    }
    else if (other.gameObject.tag == "Instant Kill")
    { //if collide with instant kill box
      TakeDamage(1000); // kill the player by passsing alot of damage
    }
    else if (other.gameObject.tag == "Coin")
    { // If collided with coin, call to collect coin
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


	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Moon" && isWolf)
    {
      anim.Play("Transform");
      isWolf = false;                 
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
  {
    yield return new WaitForSeconds(0.5f);

    anim.SetLayerWeight(1, weight);
  } // ChangeLayerWeight()

	
	void Flip ()
	{

		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
    transform.localScale = theScale;
	} // Flip()
    

  void Attacking()
  {
    isAttacking = false;
    anim.SetBool("IsAttacking", isAttacking);
    trigger.enabled = false;                      // Deactive attack trigger
  } // Attack()


  void Jumping()
  { // Play the jump animation

    isJumping = false;                      // Set jumping to false
    anim.SetBool("IsJumping", isJumping);   // Set jump animation to false

  } // Jumping()


  void Hiding()
  { // Activates/Deactivates the player hiding depending on isHide

    GetComponent<BoxCollider2D>().enabled = !isHide;
    GetComponent<CircleCollider2D>().enabled = !isHide;
    GetComponent<EdgeCollider2D>().enabled = isHide;

    if (isHide)
    {
      transform.position = new Vector3(transform.position.x, 
                                       transform.position.y, 0.1f);
    }
    else
    {
      transform.position = new Vector3(transform.position.x,
                                       transform.position.y, -0.1f);
    }

  } // Hiding()


  void EndLevel()
  { // Starts the end level sequence

    anim.SetBool("isWalking", true);
    endLevel = true;

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
  { // Play animation then destroy the enemy when has no health left

    anim.SetBool("IsDead", true);     // Play dead animation

    // Wait until animation is finished to destroy enemy
    yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).
                                    length);

    Camera.main.transform.parent = null;         // Disconnect camera
    Destroy(this.gameObject);                    // Destroy enemy
    gameOverMenu.SetActive(true);                // Show game over

  } // Die()

}
