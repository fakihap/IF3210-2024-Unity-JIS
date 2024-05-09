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
        // if (Input.GetKeyDown(KeyCode.U)) {
            UpdateDirector();
        // }
    }
    public void UpdateDirector() {
        // Debug.Log("Updating director");

        foreach(DirectableObject directable in directables) {
            // if (!directable.IsActive()) {
                directable.UpdateDirectable();
            // }
        }
    }

    public List<Quest> GetQuests() {
        // Debug.Log(directables);
        //  Debug.Log(directables.Count);
        // filter quest
        return directables.Where(x => x is Quest).Cast<Quest>().ToList();
    }
}
