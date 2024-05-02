using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class CameraTransform {
    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public class Dialog {
    public string name;
    public string text;
    public CameraTransform camTransform;            // need to be relative to the current scene
    // or maybe also animation
}

[CreateAssetMenu(fileName = "Cutscene", menuName = "Cutscene/CutsceneDialog", order = 1)]
public class CutsceneDialog : ScriptableObject
{
    
    [SerializeField]
    public List<Dialog> dialogues;

    [MenuItem("Cutscene/Copy Scene View Camera Position")]
    static public void MoveSceneViewCamera()
    {
        Debug.Log(EditorGUIUtility.systemCopyBuffer);
        Vector3 position = SceneView.lastActiveSceneView.pivot;
        Quaternion rotation = SceneView.lastActiveSceneView.rotation;

        EditorGUIUtility.systemCopyBuffer = "GenericPropertyJSON:{\"name\":\"camTransform\",\"type\":-1,\"children\":[{\"name\":\"position\",\"type\":9,\"children\":[{\"name\":\"x\",\"type\":2,\"val\":"+ position.x +"},{\"name\":\"y\",\"type\":2,\"val\":" + position.y + "},{\"name\":\"z\",\"type\":2,\"val\":" + position.z + "}]},{\"name\":\"rotation\",\"type\":17,\"val\":\"{Quaternion(" + rotation.w + ", " + rotation.x + ", " + rotation.y + ", " + rotation.z + ")}\"}]}" ;
    }
}

