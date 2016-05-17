using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreControl : MonoBehaviour
{
    private AudioSource shopAudio;

    public AudioClip posClip;
    public AudioClip negClip;

    [SerializeField]
    private Text coinText;

    void Awake()
    {
        shopAudio = GetComponent<AudioSource>();
        coinText.text = "x " + GameSaves.saves.coins;   // Update coin tracker field
    }

    void Update()
    {
        coinText.text = "x " + GameSaves.saves.coins;   // Update coin tracker field
    }
    public void IncreaseHealth()
    {
        //If enough coins are available
        if (GameSaves.saves.coins >= 100)
        {
            shopAudio.clip = posClip;
            shopAudio.Play();
            //Increase the health
            GameSaves.saves.maxHealth += 25;
            //Decrease the number of coins
            GameSaves.saves.coins -= 100;
            //Save the game
            GameSaves.saves.Save();
        }
        else
        {
            shopAudio.clip = negClip;
            shopAudio.Play();
        }

    }
}