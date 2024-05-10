using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class CurrStateData
{
    public static GameData currGameData;
    //public static bool motherlode = false;

    // for statistics 
    //public static float hitCount = 0;
    //public static float shotCount = 0;
    //public static float distanceTravelled = 0;
    //public static int damageDealt = 0;
    //public static int damageTaken = 0;
    //public static int enemyKilled = 0;

    //public static float startTime = 0;
    //public static float elapsedTime = 0;
    public static StateData stateData;
    public static CurrStateData currStateData;


    public CurrStateData()
    {
        currGameData = new GameData();
        stateData = new StateData();
        currGameData.playerName = stateData.playerName;
        currGameData.volume= stateData.volume;
        currGameData.motherlode = false;
        currGameData.hitCount = 0;
        currGameData.shotCount = 0;
        currGameData.distanceTravelled = 0;
        currGameData.damageDealt = 0;
        currGameData.damageTaken = 0;
        currGameData.enemyKilled = 0;
        currGameData.startTime = 0;
        currGameData.elapsedTime = 0;
    }

    public static CurrStateData GetInstance()
    {
        if (currStateData == null)
        {
            currStateData = new CurrStateData();
        }
        return currStateData;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(currGameData);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, currGameData);
    }

    public static void LoadStateData()
    {
        GetInstance();
        currGameData.coin = 30000;
        currGameData.pets = new List<int>();
        currGameData.currPetHealth = -1;
    }

    public static int GetCurrentCoin()
    {
        GetInstance();
        return currGameData.coin;
    }

    public static void SetCurrentCoin(int coin)
    {
        GetInstance();
        currGameData.coin = coin;
    }

    public static void AddCoin(int coin)
    {
        GetInstance();
        currGameData.coin += coin;
    }

    public static bool SubstractCoin(int coin)
    {
        GetInstance();
        if (currGameData.motherlode) return true;
        if (currGameData.coin < coin)
        {
            return false;
        }
        currGameData.coin -= coin;
        return true;
    }

    public static void InitCurrentPets()
    {
        GetInstance();
        currGameData.pets = new List<int>();
    }

    public static int GetCurrentPet()
    {
        GetInstance();
        if (currGameData.pets.Count > 0)
        {
            return currGameData.pets[0];
        }
        else
        {
            return -1;
        }
    }

    public static void RemoveCurrentPet()
    {
        GetInstance();
        if (currGameData.pets.Count > 0)
        {
            currGameData.pets.RemoveAt(0);
        }
        else
        {
            throw new Exception("You don't have any pet");
        }
    }

    public static void AddPet(int pet)
    {
        GetInstance();
        currGameData.pets.Add(pet);
    }

    public static int GetPetsLength()
    {
        GetInstance();
        return currGameData.pets.Count;
    }

    public static int GetCurrentPetHealth()
    {
        GetInstance();
        return currGameData.currPetHealth;
    }

    public static void SetCurrentPetHealth(int amount)
    {
        GetInstance();
        currGameData.currPetHealth = amount;
    }
    public static float GetShotAccuracy()
    {
        GetInstance();
        Debug.Log("shot count: " + CurrStateData.currGameData.shotCount + " hit count: " + CurrStateData.currGameData.hitCount);
        if (CurrStateData.currGameData.shotCount > 0)
            return CurrStateData.currGameData.hitCount / CurrStateData.currGameData.shotCount;
        return 0;
    }

    public static int getSecondsPlaying()
    {
        return Mathf.FloorToInt(currGameData.elapsedTime);
    }

    public static int getMinutesPlaying()
    {
        return Mathf.FloorToInt(currGameData.elapsedTime / 60f);
    }

    public static int getHoursPlaying()
    {
        return Mathf.FloorToInt(currGameData.elapsedTime / 3600f);
    }

    public static int GetVolume()
    {
        return stateData.volume;
    }

    public static void ChangeVolume(int amount)
    {
        stateData.volume += amount;
        stateData.volume = Math.Min(Math.Max(stateData.volume, 0), 100);
    }

    public static string GetPlayerName()
    {
        return currGameData.playerName;
    }

    public static void ChangePlayerName(string name)
    {
        currGameData.playerName = name;
    }

    public static void SetDifficultyLevel(int difficultyIndex)
    {
        if(difficultyIndex == 0)
        {
            currGameData.difficultyLevel = "easy";
        }
        else if(difficultyIndex == 1)
        {
            currGameData.difficultyLevel = "medium";
        }
        else
        {
            currGameData.difficultyLevel = "hard";
        }
    }

    public static string GetStateDataDifficulty()
    {
        return stateData.difficulty;
    }

    public static string GetDifficultyLevel()
    {
        return currGameData.difficultyLevel;
    }

    public static int GetDifficultyLevelIndex()
    {
        if(currGameData.difficultyLevel == "easy")
        {
            return 0;
        }
        else if(currGameData.difficultyLevel == "medium")
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public static void UpdateStateData()
    {
        stateData.playerName = currGameData.playerName;
        stateData.volume = currGameData.volume;
        stateData.difficulty = currGameData.difficultyLevel;
    }
}