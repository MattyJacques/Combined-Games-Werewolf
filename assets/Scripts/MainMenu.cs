using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

  public enum MenuState { Main, LevelSelect };
  public MenuState currentState;

  public GameObject StartMenu;
  public GameObject LevelSelect;



  // Use this for initialization
  void Start()
  {
    currentState = MenuState.Main;
    StartMenu.SetActive(true);
    LevelSelect.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnMainMenu()
  {
    StartMenu.SetActive(true);
  }

  void OnLevelSelect()
  {
    LevelSelect.SetActive(true);
    StartMenu.SetActive(false);
  }

  public void LoadLevel(string levelName)
  {
    SceneManager.LoadScene(levelName);
    // Application.LoadLevel(levelName);
  }


}
