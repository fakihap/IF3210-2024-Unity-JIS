using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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

[CreateAssetMenu(fileName = "Cutscene01", menuName = "Directable Objects/New Dialogue Cutscene", order = 0)]
public class Cutscene : DirectableObject
{
    int currentIndex = -1; // -1 means not yet started
    bool isCompleted = false;
    string previousSceneName;

    [SerializeField]
    List<Dialogue> dialogues;

    [SerializeField]
    string sceneName; // need to exist
    
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

    public void EndCutscene() {
        isCompleted = true;
    }

    int MaxIndex() {
        return dialogues.Count;
    }

    public override bool IsActive()
    {
        return currentIndex >= 0; // need better alternative
    }

    public override bool IsCompleted()
    {
        return isCompleted;
    }

    protected override void StartDirectable()
    {
        // SceneManager.LoadScene(sceneName, LoadSceneMode.Additive); // may result in error missing scene name

        previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName); // may result in error missing scene name
    }

    protected override void EndDirectable() {
        Debug.Log("SOMEHOWN ENDING THIS CTWSC");
        SceneManager.LoadScene(previousSceneName);
    }

    public override void ResetDirectable()
    {
        isCompleted = false;

        currentIndex = -1;
    }
}


