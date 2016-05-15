using UnityEngine;
using System.Collections;

public class StoreControl : MonoBehaviour
{

    public void IncreaseHealth()
    {
        if (GameSaves.saves.coins >= 100)
        {
            GameSaves.saves.maxHealth += 25;
            GameSaves.saves.coins -= 100;
            GameSaves.saves.Save();
        }
    }
}