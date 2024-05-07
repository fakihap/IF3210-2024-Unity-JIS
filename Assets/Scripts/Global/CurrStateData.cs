using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class CurrStateData
{
    private static GameData currGameData;

    public static void LoadStateData()
    {
        currGameData.coin = 30000;
        currGameData.pets = new List<int>();
        currGameData.currPetHealth = -1;
    }

    public static int GetCurrentCoin()
    {
        return currGameData.coin;
    }

    public static void SetCurrentCoin(int coin)
    {
        currGameData.coin = coin;
    }

    public static void AddCoin(int coin)
    {
        currGameData.coin += coin;
    }

    public static bool SubstractCoin(int coin)
    {
        if(currGameData.coin < coin)
        {
            return false;
        }
        currGameData.coin -= coin;
        return true;
    }

    public static void InitCurrentPets()
    {
        currGameData.pets = new List<int>();
    }

    public static int GetCurrentPet()
    {
        if(currGameData.pets.Count > 0)
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
        if(currGameData.pets.Count > 0)
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
        currGameData.pets.Add(pet);
    }

    public static int GetPetsLength()
    {
        return currGameData.pets.Count;
    }

    public static int GetCurrentPetHealth()
    {
        return currGameData.currPetHealth;
    }

    public static void SetCurrentPetHealth(int amount)
    {
        currGameData.currPetHealth = amount;
    }
}