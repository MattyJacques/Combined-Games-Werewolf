using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndSign : MonoBehaviour
{
  public string levelName = ""; //Name to be passed into script, 
                           //cleaner way of doing this but just for now.


	// Use this for initialization
	void Start ()
  {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


  //on trigger enter load the next level
  void OnTriggerEnter2D(Collider2D other)
  {
    if(other.gameObject.tag == "Player")
    {
      SceneManager.LoadScene(levelName);
    }
  }
}
