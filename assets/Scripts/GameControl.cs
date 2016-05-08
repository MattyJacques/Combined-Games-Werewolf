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
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            if (player.GetComponent<PlayerController>().health <= 0)
            {
                Camera.main.transform.parent = null;
                Destroy(player.gameObject);
                gameOverMenu.SetActive(true);
            }
        }
	
	}

    public void PauseGame ()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(paused);
        }
        else
        {
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
