using UnityEngine;
using UnityEditor.Animations;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public bool facingRight = true;		       // Is player facing right

	public float moveSpeed = 1.5f;				   // Move speed of player
	public float jumpForce = 1000f;          // The jump force of player

  public Transform ground;
  private bool isGrounded = false;

  public int health = 200;                 // Current health of player

	private Animator anim;                   // Animation controller
  private Rigidbody2D rb;                  // Rigidbody of player
  public BoxCollider2D trigger;            // Holds the player attack trigger

	private bool isWolf = true;              // Is the player a wolf
  private bool isWalking = false;          // Is the player walking
  private bool isAttacking = false;        // Is the player attacking
  private bool isJumping = false;          // Is the player jumping
  private bool isDead = false;             // If current player state is dead


	void Awake ()
	{ // Get the animation controller and rigidbody on object creation, disable 
    // the attack trigger

		anim = GetComponent<Animator> ();       // Get animation controller
    rb = GetComponent<Rigidbody2D>();       // Get rigidbody 

    trigger.enabled = false;                // Disable attack trigger

	} // Awake()

	void Update ()
	{

    // Maybe change to FixedUpdate
    isGrounded = Physics2D.Linecast(transform.position, ground.position, 1 
                   << LayerMask.NameToLayer("Ground"));

		if (Input.GetButtonDown ("Jump") && isGrounded) //Add grounded "&& grounded"
    {
            isJumping = true;
            anim.SetBool("IsJumping", isJumping);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
    }

    if (Input.GetButtonDown("Fire1") && !isJumping)
    {
      isAttacking = true;
      anim.SetBool("IsAttacking", isAttacking);

      trigger.enabled = true;                      // Enable attack trigger
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
    if (!isAttacking)
    {
      float h = Input.GetAxis("Horizontal");

      if (h != 0)
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
      anim.SetBool("IsWalking", isWalking);

      if (h > 0 && !facingRight)
      {
        Flip();
      }
      else if (h < 0 && facingRight)
      {
        Flip();
      }
    }
	} // FixedUpdate()


	void OnTriggerEnter2D (Collider2D other)
  { // Checks to see if the moon is entering the player trigger, if so transform
   // into a wolf

    if (other.gameObject.tag == "Moon" && !isWolf)
    { // If the object is the moon and the player is not human, change to wolf

      anim.Play("Transform");                       // Go to transform state
      isWolf = true;                                // Set wolf to true
      StartCoroutine(ChangeLayerWeight(0f));        // Change to wolf animation
		}

	} // OnTriggerEnter2D()


	void OnTriggerExit2D (Collider2D other)
	{ // Checks to see if the moon is leaving the player trigger, if so, transform
    // into a human

		if (other.gameObject.tag == "Moon" && isWolf)
    { // If the object is the moon and the player is a wolf, change to human

      anim.Play("Transform");                       // Go to transform state
      isWolf = false;                               // Set wolf to false
      StartCoroutine(ChangeLayerWeight(1f));        // Change to human animation
    }

	} // OnTriggerExit2D()


  IEnumerator ChangeLayerWeight(float weight)
  { // Changes the weight of the layer after half second to let the transform
    // animation start to play first

    yield return new WaitForSeconds(0.5f);          // Return in 0.5 seconds

    anim.SetLayerWeight(1, weight);                 // Set layer weight

  } // ChangeLayerWeight()

	
  void Flip ()
  { // Flip the player sprite to face the direction of travel

    facingRight = !facingRight;                       // Set the bool to opposite

    Vector3 theScale = transform.localScale;          // Get current scale
    theScale.x *= -1;                                 // Get opposite of scale
    transform.localScale = theScale;                  // Set new scale

	  } // Flip()
    

  void Attacking()
  { // Stops the player attack animation and deactivates attack trigger

    isAttacking = false;                          // Stop player attack
    anim.SetBool("IsAttacking", isAttacking);     // Stop attack animation
    trigger.enabled = false;                      // Deactive attack trigger

  } // Attacking()


  void Jumping()
  { // Play the jump animation

    isJumping = false;                      // Set jumping to false
    anim.SetBool("IsJumping", isJumping);   // Set jump animation to false

  } // Jumping()


  public void TakeDamage(int damage)
  { // Subtract damage given as parameter from the current player health

    health -= damage;                       // Subtract damage from health

    if (health <= 0)
    { // If health is equal or less than 0, kill the player
      isDead = true;                        // Set dead to true
      Die();                                // Call to kill player
    }

  } // TakeDamage()


  void Die()
  { // Play animation then destroy the enemy when has no health left

    anim.SetBool("IsDead", isDead);         // Play dead animation

    // Wait until animation is finished to destroy enemy
    //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).
    //                                length);
    
    //Destroy(this);                        // Destroy enemy

  } // Die()

}
