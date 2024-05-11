using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

[System.Serializable]
public struct GameData
{
    public string playerName;
    public string difficultyLevel;
    public int volume;
    public int coin;
    public List<int> pets;
    public int currPetHealth;
    public List<int> petHealth;
    public bool motherlode;
    public float hitCount;
    public float shotCount;
    public float distanceTravelled;
    public int damageDealt;
    public int damageTaken;
    public int enemyKilled;
    public float startTime;
    public float elapsedTime;
    public int currentSlot;

    public Vector3 playerCoordinates;
    public float playerHealth;

    public List<KerocoState> kerocoList;
    public List<KepalaKerocoState> kepalaKerocoList;
    public List<JendralState> jendralList;
    public List<RajaState> rajaList;

# region GameDirector
    public bool[] directablesCompletion;
# endregion

}

[System.Serializable]
public struct KerocoState
{
    public Vector3 coordinate;
    public float health;
}

[System.Serializable]
public struct KepalaKerocoState
{
    public Vector3 coordinate;
    public float health;
}

[System.Serializable]
public struct JendralState
{
    public Vector3 coordinate;
    public float health;
}

[System.Serializable]
public struct RajaState
{
    public Vector3 coordinate;
    public float health;
}