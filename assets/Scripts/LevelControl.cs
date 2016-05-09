using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {

    public void ChangeToLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
