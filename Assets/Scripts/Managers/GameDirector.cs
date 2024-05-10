using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance;

    [Header("Directables")]
    // we won't use index since a lot of things can happen concurrently
    // instead we will check if each directables is finished 
    // and propagate the result to the succeeding directable
    [SerializeField] List<DirectableObject> directables;

    void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    void Start() {
        foreach(DirectableObject directable in directables) {
            directable.ResetDirectable();
        }
    }

    void Update() {
        UpdateDirector();
    }
    public void UpdateDirector() {

        foreach(DirectableObject directable in directables) {
            directable.UpdateDirectable();
        }
    }

    public List<Quest> GetQuests() {
        // filter quest
        return directables.Where(x => x is Quest).Cast<Quest>().ToList();
    }

# region Saving Directables State
    // TODO : make it supports saving to certain save slot
    // WARNING : UNTESTED
    public void SaveDirectables() {
        CurrStateData.SaveGameProgress(directables);
    }

    public void LoadDirectables() {
        CurrStateData.LoadGameProgress(ref directables);
    }
# endregion
}
