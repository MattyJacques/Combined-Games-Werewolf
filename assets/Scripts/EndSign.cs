using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndSign : MonoBehaviour
{
  public string levelName = ""; //Name to be passed into script, 
                                //cleaner way of doing this but just for now

  private GameObject player;    //gameobject player

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player"); //find player inscene
  }

  void FixedUpdate ()
  {
        if (player != null) //if player is not null
        {
            //if player is not visble and level is ended
            if (!player.GetComponent<SpriteRenderer>().isVisible && 
                player.GetComponent<PlayerController>().endLevel)
            {
                GameSaves.saves.Save();            //GameSaves
                SceneManager.LoadScene(levelName); //load scene
                Debug.Log("Level ended");          // debug
            }
        }
  }


  //on trigger enter load the next level
  void OnTriggerEnter2D(Collider2D other)
  {
    if(other.gameObject.tag == "Player")
    {
        //camera
      Camera.main.transform.parent = null;
      other.SendMessage("EndLevel");

    }
  }
}
