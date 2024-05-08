using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using Unity.VisualScripting;
using UnityEngine;

public class CurrStateData
{
    private static GameData currGameData;
    public static bool motherlode = false;

    // for statistics 
    public static float hitCount = 0;
    public static float shotCount = 0;
    public static float distanceTravelled = 0;
    static CurrStateData currStateData;
    private float startTime;
    private float elapsedTime;

    public static CurrStateData GetInstance() {
        if (currStateData == null) {
            currStateData = new CurrStateData();
        }
        return currStateData;
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
        if (motherlode) return true;
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
        Debug.Log("shot count: " + CurrStateData.shotCount + " hit count: " + CurrStateData.hitCount);
        if (CurrStateData.shotCount > 0)
            return CurrStateData.hitCount / CurrStateData.shotCount;
        return 0;
    }
}