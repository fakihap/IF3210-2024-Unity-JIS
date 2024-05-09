using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CameraTransform {
    public Vector3 position;
    public Vector3 rotation;
}

[Serializable]
public class Dialogue {
    public string name;
    public string text;
    public CameraTransform camTransform;            // need to be relative to the current scene
    // or maybe also animation
}

[CreateAssetMenu(fileName = "Cutscene", menuName = "Cutscene/Cutscene Dialog", order = 1)]
public class Cutscene : ScriptableObject
{
    int currentIndex = -1; // -1 means not yet started

    [SerializeField]
    List<Dialogue> dialogues;
    
    public bool NextDialogue() {
        if (currentIndex + 1 >= MaxIndex()) {
            return false;
        }

        currentIndex += 1;
        return true;
    }

    public Dialogue GetCurrentDialogue() {
        return dialogues[currentIndex];
    }

    int MaxIndex() {
        return dialogues.Count;
    }
}


