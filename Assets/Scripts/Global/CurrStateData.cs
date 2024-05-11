using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nightmare;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class CurrStateData
{
    public static GameData currGameData;

    public static CurrStateData currStateData;


    public CurrStateData(int slotNumber)
    {
        currGameData = new GameData();
        // stateData = new StateData();
        // currGameData.playerName = stateData.playerName;
        // currGameData.volume= stateData.volume;
        currGameData.motherlode = false;
        currGameData.hitCount = 0;
        currGameData.shotCount = 0;
        currGameData.distanceTravelled = 0;
        currGameData.damageDealt = 0;
        currGameData.damageTaken = 0;
        currGameData.enemyKilled = 0;
        currGameData.startTime = 0;

        Debug.Log("I'm changing the slot number to " + slotNumber);
        currGameData.currentSlot = slotNumber;
        currGameData.elapsedTime = 0;
    }

    public static CurrStateData InstantiateNewData()
    {
        // Check if slot 1, slot 2, and slot 3 exists, if all exists overwrite slot 1
        var slot1Path = Path.Combine(Application.persistentDataPath, "Slot1.dat");
        var slot2Path = Path.Combine(Application.persistentDataPath, "Slot2.dat");
        var slot3Path = Path.Combine(Application.persistentDataPath, "Slot3.dat");

        if (!File.Exists(slot1Path))
        {
            currStateData = new CurrStateData(1);
            Debug.Log("Creating slot 1");
        } else if (!File.Exists(slot2Path))
        {
            currStateData = new CurrStateData(2);
            Debug.Log("Creating slot 2");
        } else if (!File.Exists(slot3Path))
        {
            currStateData = new CurrStateData(3);
            Debug.Log("Creating slot 3");
        } else
        {
            currStateData = new CurrStateData(1);
            Debug.Log("Overwriting slot 1");
        }

        return currStateData;
    }

    public static CurrStateData GetInstance()
    {
        if (currStateData == null)
        {
            currStateData = new CurrStateData(1);
        }
        return currStateData;
    }

    public static string ToJson()
    {
        return JsonUtility.ToJson(currGameData);
    }

    public static void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, currGameData);
    }

    public static void LoadStateData()
    {
        //GetInstance();
        currGameData.coin = 30000;
        currGameData.pets = new List<int>();
        currGameData.petHealth = new List<int>();
        currGameData.currPetHealth = -1;
    }

    public static int GetCurrentCoin()
    {
        //GetInstance();
        return currGameData.coin;
    }

    public static void SetCurrentCoin(int coin)
    {
        //GetInstance();
        currGameData.coin = coin;
    }

    public static void AddCoin(int coin)
    {
        //GetInstance();
        currGameData.coin += coin;
    }

    public static bool SubstractCoin(int coin)
    {
        //GetInstance();
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
        //GetInstance();
        currGameData.pets = new List<int>();
        currGameData.petHealth = new List<int>();
    }

    public static bool HasPetAttacker(){
        for(int i = 0; i < CurrStateData.GetPetsLength(); i++)
        {
            if(currGameData.pets[i] == 0)
            {
                return true;
            }
        }
        return false;
    }

    public static bool HasPetHealer(){
        for(int i = 0; i < CurrStateData.GetPetsLength(); i++)
        {
            if(currGameData.pets[i] == 1)
            {
                return true;
            }
        }
        return false;
    }

    public static int GetCurrentPet()
    {
        //GetInstance();
        if (currGameData.pets.Count > 0)
        {
            return currGameData.pets[0];
        }
        else
        {
            return -1;
        }
    }

    public static int GetNextPet()
    {
        GetInstance();
        if(currGameData.pets.Count > 1)
        {
            return currGameData.pets[1];
        }
        else
        {
            return -1;
        }
    }

    public static int GetPetAtIndex(int i)
    {
        GetInstance();
        if(i >= 0 && i < currGameData.pets.Count)
        {
            return currGameData.pets[i];
        }
        else
        {
            return -1;
        }
    }

    public static void RemoveCurrentPet()
    {
        //GetInstance();
        if (currGameData.pets.Count > 0)
        {
            currGameData.pets.RemoveAt(0);
            currGameData.petHealth.RemoveAt(0);
            if (currGameData.petHealth.Count > 0) {
                currGameData.currPetHealth = currGameData.petHealth[0];
            } else  {
                currGameData.currPetHealth = -1;
            }
        }
        else
        {
            throw new Exception("You don't have any pet");
        }
    }

    public static void AddPet(int pet)
    {
        //GetInstance();
        currGameData.petHealth.Add(100);
        currGameData.pets.Add(pet);
    }

    public static void SwitchPets()
    {
        if (currGameData.pets.Count >= 2)
        {
            int temp = currGameData.pets[0];
            currGameData.pets[0] = currGameData.pets[1];
            currGameData.pets[1] = temp;

            currGameData.petHealth[0] = currGameData.petHealth[1];
            currGameData.petHealth[1] = currGameData.currPetHealth; 
            currGameData.currPetHealth = currGameData.petHealth[0];
        }
        else
        {
            Debug.LogError("Not enough pets to switch.");
        }
    }

    public static int GetPetsLength()
    {
        //GetInstance();
        return currGameData.pets.Count;
    }

    public static int GetCurrentPetHealth()
    {
        //GetInstance();
        return currGameData.currPetHealth;
    }

    public static void SetCurrentPetHealth(int amount)
    {
        //GetInstance();
        currGameData.currPetHealth = amount;
    }
    public static float GetShotAccuracy()
    {
        //GetInstance();
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

    // public static int GetVolume()
    // {
    //     return stateData.volume;
    // }

    // public static void ChangeVolume(int amount)
    // {
    //     stateData.volume += amount;
    //     stateData.volume = Math.Min(Math.Max(stateData.volume, 0), 100);
    // }

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

        //change enemy manager
        EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        enemyManager.ChangeLevel(difficultyIndex);
    }

    // public static string GetStateDataDifficulty()
    // {
    //     return stateData.difficulty;
    // }

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

    // public static void UpdateStateData()
    // {
    //     stateData.playerName = currGameData.playerName;
    //     stateData.volume = currGameData.volume;
    //     stateData.difficulty = currGameData.difficultyLevel;
    // }

# region GameDirector
    public static void SaveGameProgress(List<DirectableObject> directables) {
        currGameData.directablesCompletion = directables.Select((directable, _) => directable.IsCompleted()).ToArray();
    }

    public static void LoadGameProgress(ref List<DirectableObject> directables) {
        // check if they have the same length of data
        // error may occur if we change quests data
        // TODO : have it make an assumption, 
        //        ensuring it always works even though with unexpected behavior
        if (directables.Count != currGameData.directablesCompletion.Length) {
            Debug.LogError("CurrStateData : difference length of directables list and gamedata completion array");
        }

        // update completion for each directables
        for (int i = 0; i < directables.Count; i++) {
            directables[i].SetCompletion(currGameData.directablesCompletion[i]);
        }   
    }
# endregion

}