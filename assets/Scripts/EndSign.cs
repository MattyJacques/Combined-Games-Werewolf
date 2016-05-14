using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndSign : MonoBehaviour
{
  public string levelName = ""; //Name to be passed into script, 
                                //cleaner way of doing this but just for now.

  private GameObject player;

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
  }

  void FixedUpdate ()
  {
	  if (!player.GetComponent<SpriteRenderer>().isVisible)
    {
      //SceneManager.LoadScene(levelName);
      Debug.Log("Level ended");
    }
  }


  //on trigger enter load the next level
  void OnTriggerEnter2D(Collider2D other)
  {
    if(other.gameObject.tag == "Player")
    {
      Camera.main.transform.parent = null;
      other.SendMessage("EndLevel");

    }
  }
}
