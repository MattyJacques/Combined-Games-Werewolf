using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {

    public void ChangeToLevel(string level)
    {
        //Save the game
        GameSaves.saves.Save();
        //Load the set level
        SceneManager.LoadScene(level);
    }

    public void DeleteSave()
    {
        //Delete the save
        GameSaves.saves.Delete();
        //Create a new save file
        GameSaves.saves.Load();
    }
}
