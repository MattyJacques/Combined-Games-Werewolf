using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

  public enum MenuState { Main, LevelSelect }; //menu states
  public MenuState currentState;               //current menu state

  public GameObject StartMenu;                 //gameobject contains main menu
  public GameObject LevelSelect;               //gameobject contains level menu

  // Use this for initialization
  void Start()
  {
    //currentState = MenuState.Main; 
    StartMenu.SetActive(true);                //activate main menu
    LevelSelect.SetActive(false);             //deactivate level select
  }//Start()

  public void OnMainMenu()
  {
    StartMenu.SetActive(true);                //activate main menu
    LevelSelect.SetActive(false);             //deactivate level select
  }//OnMainMenu

  public void OnLevelSelect()
  {
    LevelSelect.SetActive(true);                //activate level select
    StartMenu.SetActive(false);                 //deactivate main menu
  }//OnLevelSelect()

  public void LoadLevel(string levelName)
  {
    GameSaves.saves.Save();                   //save game
    SceneManager.LoadScene(levelName);        //load level name
  }//LoadLevel()

    public void QuitGame()
    {
        GameSaves.saves.Save(); //save game
        Application.Quit();     //quit game
    }//QuitGame()


}
