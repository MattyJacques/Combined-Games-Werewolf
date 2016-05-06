using UnityEngine;
using UnityEditor.Animations;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public bool facingRight = true;		

	public float moveSpeed = 1.5f;				
	public float jumpForce = 1000f;

  public int health = 200;                 // Current health of player

	private Animator anim;
  private Rigidbody2D rb;
  public BoxCollider2D trigger;            // Holds the player attack trigger

  public Sprite humanSprite;
  public Sprite wolfSprite;

	private bool isWolf = true;
  private bool isWalking = false;
  private bool isAttacking = false;
  private bool isJumping = false;
  private bool isDead = false;             // If current player state is dead


	void Awake ()
	{
		anim = GetComponent<Animator> ();
    rb = GetComponent<Rigidbody2D>();

    trigger.enabled = false;                // Disable attack trigger
	}

	void Update ()
	{
		if (Input.GetButtonDown ("Jump")) //Add grounded "&& grounded"
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

	}


	void FixedUpdate ()
	{
        if (isJumping)
        {
            if (Physics2D.Raycast(transform.position, -Vector2.up, 0.2f) && (rb.velocity.y < 0))
            {
                //FIX
                Debug.DrawRay(transform.position, -Vector2.up);
                isJumping = false;
                anim.SetBool("IsJumping", isJumping);
            }
        }
        if (!isAttacking)
        {
            float h = Input.GetAxis("Horizontal");

            if (h != 0)
            {
                isWalking = true;

                transform.position = new Vector3(transform.position.x + (h * Time.deltaTime * moveSpeed), transform.position.y, transform.position.z);
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
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Moon" && !isWolf) {
      anim.Play("Transform");
      isWolf = true;
      //anim.SetBool ("IsWolf", isWolf);
      //anim.SetLayerWeight(1, 0f);                   // Change to wolf animation
      Debug.Log("Setting weight to 0");
      StartCoroutine(ChangeLayerWeight(0f));
		}

	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Moon") {
      anim.Play("Transform");
      isWolf = false;
      //anim.SetBool ("IsWolf", isWolf);
      //anim.SetLayerWeight(1, 1f);                   // Change to human animation
      Debug.Log("Setting weight to 1");
      StartCoroutine(ChangeLayerWeight(1f));
    }
	}

  IEnumerator ChangeLayerWeight(float weight)
  {
    yield return new WaitForSeconds(0.5f);

    anim.SetLayerWeight(1, weight);
  }

	
	void Flip ()
	{

		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
    //anim.SetLayerWeight(1, 1f);
    transform.localScale = theScale;
	}
    
  void Attacking()
  {
    isAttacking = false;
    anim.SetBool("IsAttacking", isAttacking);
    trigger.enabled = false;                      // Deactive attack trigger
  }

  void Jumping()
  {
      isJumping = false;
      anim.SetBool("IsJumping", isJumping);
  }


  public void TakeDamage(int damage)
  { // Subtract damage given as parameter from the current player health

    health -= damage;

    if (health <= 0)
    {
      isDead = true;
      Die();
    }

  } // TakeDamage()


  void Die()
  { // Play animation then destroy the enemy when has no health left

    anim.SetBool("IsDead", isDead);     // Play dead animation

    // Wait until animation is finished to destroy enemy
    //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).
    //                                length);
    
    //Destroy(this);                             // Destroy enemy

  } // Die()

}
