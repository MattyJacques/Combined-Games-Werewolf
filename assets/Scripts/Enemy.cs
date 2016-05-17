using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public int health; //health of enemy
    public bool scary; //is enemy scary
    public float speed = 15; //enemy speed
    public int damage = 10; //damage of enemy
    public int rangeCheck = 20; //checked Range from player
    public GameObject player;
    public SpriteRenderer healthBar;     // prite renderer of the health bar
    private Vector3 healthScale;         // The local scale of the health bar
                                         // initially (with full health)
    public GameObject coinSpawner;       // coinspawner object

    void Start()
    {
        health = 100; //set health to 100
        scary = false;//set scary bool
        coinSpawner.SetActive(false); //deactivate spawner
        player = GameObject.FindGameObjectWithTag("Player"); //find player
        healthScale = healthBar.transform.localScale;  //scale of health      
    }

    public IEnumerator Die()
    { // Play animation then destroy the enemy when has no health left
        GetComponent<Animator>().SetBool("IsDead", true);// Play dead animation

        // Wait until animation is finished to destroy enemy
        yield return new WaitForSeconds(GetComponent<Animator>().
                                        GetCurrentAnimatorStateInfo(0).length);

        Destroy(this.gameObject);                             // Destroy enemy
        coinSpawner.SetActive(true);                          //activate CS
        coinSpawner.SendMessage("SpawnCoins");                //spawn coins
    } // Die()


    public void TakeDamage(int damage)
    { // Subtract damage given as parameter from the current enemy health

        health -= damage;              // Subtract damage from health
        UpdateHealthBar();             //update health
        if (health <= 0 && healthBar != null)
        { // If health is equal or less than 0, kill the player and break bar
            Destroy(healthBar.gameObject);
            StartCoroutine(Die());       // Call to kill player
        }
    } // TakeDamage()


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Instant Kill")
        {
            //   UpdateHealthBar();
            StartCoroutine(Die());
        }
    }

    public void UpdateHealthBar()
    {
        if (health > 0)
        {
           // Set the health bar's colour to proportion of the way between
           //green and red based on the player's health.
            healthBar.material.color = Color.Lerp(Color.green, 
                                                  Color.red, 
                                                  1 - health * 0.01f);

           // Set the scale of the health bar to be proportional to 
          // the player's health.
            healthBar.transform.localScale = new Vector3(healthScale.x 
                                                         * health * 
                                                         0.01f, 1, 1);
        }


    }

    void SpawnCoins()
    {
        coinSpawner.SetActive(true);
        coinSpawner.SetActive(false);
    }
}
