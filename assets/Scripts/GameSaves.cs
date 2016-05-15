using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameSaves : MonoBehaviour
{
    public static GameSaves saves;
    public int levelUnlocked;
    public int coins;
    public float volume;
    public int maxHealth;


    void Awake()
    {
        if (saves == null)
        {
            DontDestroyOnLoad(gameObject);
            saves = this;
        }
        else if (saves != this)
        {
            Destroy(gameObject);
        }
        Load();
    }


    public void Save()
    {
        //Initialize a new Binary Formatter and create a new save file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Save.dat");

        //Create a new PlayerData
        PlayerData data = new PlayerData();
        //Set the variables according to current progress
        data.levelUnlocked = levelUnlocked;
        data.coins = coins;
        data.volume = volume;
        data.maxHealth = maxHealth;

        //Store data in the previous created file
        bf.Serialize(file, data);
        file.Close();
    }



    public void Load()
    {
        //If a save currently exists
        if (File.Exists(Application.persistentDataPath + "/Save.dat"))
        {
            //Load the file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Save.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            levelUnlocked = data.levelUnlocked;
            coins = data.coins;
            volume = data.volume;
            AudioListener.volume = volume;
            maxHealth = data.maxHealth;

        }
        else
        {
            //If no save exist create a new save
            levelUnlocked = 1;
            coins = 0;
            maxHealth = 200;

            Save();
        }
    }

    public void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/Save.dat"))
        {
            File.Delete(Application.persistentDataPath + "/Save.dat");
            Debug.Log("Save Deleted.");
        }
        else {
            Debug.LogError("GameSaves: No save can be found to delete");
        }
    }
}

[Serializable]
class PlayerData
{
    public int levelUnlocked;
    public int coins;
    public float volume;
    public int maxHealth;
}

