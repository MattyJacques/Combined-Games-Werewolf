using UnityEngine;
using System.Collections;

public class StoreControl : MonoBehaviour
{

    public void IncreaseHealth()
    {
        //If enough coins are available
        if (GameSaves.saves.coins >= 100)
        {
            //Increase the health
            GameSaves.saves.maxHealth += 25;
            //Decrease the number of coins
            GameSaves.saves.coins -= 100;
            //Save the game
            GameSaves.saves.Save();
        }
    }
}