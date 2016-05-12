using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {

    public void ChangeToLevel(string level)
    {
        GameSaves.saves.Save();
        SceneManager.LoadScene(level);
    }

    public void DeleteSave()
    {
        GameSaves.saves.Delete();
        GameSaves.saves.Load();
    }
}
