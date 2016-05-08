using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    private GameObject player;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

   

    private bool paused;

	void Start () {

        paused = false;
        player = GameObject.FindGameObjectWithTag("Player");

        if(pauseMenu == null)
        {
            Debug.LogError("Set pause menu");
        }
        if (gameOverMenu == null)
        {
            Debug.LogError("Set game over menu");
        }
    }
	
	void Update () {
        //if (player != null)
        //{
        //    //If the player dies
        //    if (player.GetComponent<PlayerController>().health <= 0)
        //    {
        //        //Detach the camera and destory the player
        //        //EDIT TO WORK WITH PLAYER DEATH ANIMATION
        //        Camera.main.transform.parent = null;
        //        Destroy(player.gameObject);
        //        gameOverMenu.SetActive(true);
        //    }
        //}
	
	}

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
