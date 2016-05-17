using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

  private GameObject player;
  public GameObject pauseMenu;

  [SerializeField]
  private Image health;
  [SerializeField]
  private Text coinText;
  
  private int maxHealth;

  private bool paused;

	void Start ()
  {
    //Set paused to be false
    paused = false;
    //Set reference to player
    player = GameObject.FindGameObjectWithTag("Player");
    //Load and set the players max health
    maxHealth = player.GetComponent<PlayerController>().health;
    // Update coin tracker field
    coinText.text = "x " + GameSaves.saves.coins;   

    //Check we have a pause menu
    if (pauseMenu == null)
    {
        Debug.LogError("Set pause menu");
    }

  }
	
	void Update ()
  {
        //Check a player exsits
        if (player != null)
        {
            // Update player health bar
            health.fillAmount = CalcHealthFill(player.GetComponent<PlayerController>().
                                               health, maxHealth);
        }
	} // Update()

  float CalcHealthFill(float currHealth, int maxHealth)
  { // Calculates how filled the health bar should be using the current health
    // and the maximum health, max health as parameter for use with enemies and
    // player

    return currHealth / maxHealth;

  } // CalcHealthFill()

  void AddCoin(GameObject coin)
  { // Add a coin to the coin tracker text field then destroy collected coin

    GameSaves.saves.coins++;                        // Increment coins collected
    coinText.text = "x " + GameSaves.saves.coins;   // Update coin tracker field
    Destroy(coin);                                  // Destroy collected coin           

  } // AddCoin()

    public void PauseGame ()
    {

        //If not paused 
        if (!paused)
        {
            //Pause the game
            paused = true;
            //Stop the timescale
            Time.timeScale = 0;
            //Enable the pause menu
            pauseMenu.SetActive(paused);
        }
        else
        {
            //Un-pause the game
            paused = false;
            //Start the timescale
            Time.timeScale = 1;
            //Disable the pause menu
            pauseMenu.SetActive(paused);
        }
    }

    public void Menu()
    {
        //Save the game
        GameSaves.saves.Save();
        //Start the timescale
        Time.timeScale = 1;
        //Load the menu
        SceneManager.LoadScene("Menu");
    }

    public void Options()
    {
        //Save the game
        GameSaves.saves.Save();
        //Start the timescale
        Time.timeScale = 1;
        //Load the options menu
        SceneManager.LoadScene("Options");
    }

    public void Restart()
    {
        //Save the game
        GameSaves.saves.Save();
        //Start the timescale
        Time.timeScale = 1;
        //Reload the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
