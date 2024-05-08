using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

[System.Serializable]
public struct GameData
{
    public int coin;
    public List<int> pets;
    public int currPetHealth;
    public bool motherlode;
    public float hitCount;
    public float shotCount;
    public float distanceTravelled;
    public int damageDealt;
    public int damageTaken;
    public int enemyKilled;
    public float startTime;
    public float elapsedTime;
}