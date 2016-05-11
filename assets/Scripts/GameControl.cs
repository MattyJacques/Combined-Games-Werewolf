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

  private int coinNum = 0;
  
  private int maxHealth;

  private bool paused;

	void Start ()
  {

    paused = false;
    player = GameObject.FindGameObjectWithTag("Player");
    maxHealth = player.GetComponent<PlayerController>().health;

    if(pauseMenu == null)
    {
        Debug.LogError("Set pause menu");
    }

    AddCoin();
  }
	
	void Update ()
  {

    // Update player health bar
    health.fillAmount = CalcHealthFill(player.GetComponent<PlayerController>().
                                       health, maxHealth);
	
	} // Update()

  float CalcHealthFill(float currHealth, int maxHealth)
  { // Calculates how filled the health bar should be using the current health
    // and the maximum health, max health as parameter for use with enemies and
    // player

    return currHealth / maxHealth;

  } // CalcHealthFill()

  void AddCoin()
  { // Add a coin to the coin tracker text field

    coinText.text = "x " + coinNum;

  } // AddCoin()

    public void PauseGame ()
    {

        //If not paused 
        if (!paused)
        {
            //Pause the game
            paused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(paused);
        }
        else
        {
            //Un-pause the game
            paused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(paused);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

  public void LoadLevel()
  {

  }
}
