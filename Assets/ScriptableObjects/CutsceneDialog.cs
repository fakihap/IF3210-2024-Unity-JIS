using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class CameraTransform {
    public Vector3 position;
    public Vector3 rotation;
}

[Serializable]
public class Dialog {
    public string name;
    public string text;
    public CameraTransform camTransform;            // need to be relative to the current scene
    // or maybe also animation
}

[CreateAssetMenu(fileName = "Cutscene", menuName = "Cutscene/Cutscene Dialog", order = 1)]
public class CutsceneDialog : ScriptableObject
{
    
    [SerializeField]
    public List<Dialog> dialogues;

    [MenuItem("Cutscene/Copy Scene View Camera Transform")]
    static public void CopySceneViewCameraTransform()
    {
        Vector3 position = SceneView.lastActiveSceneView.pivot;
        Vector3 rotation = SceneView.lastActiveSceneView.rotation.eulerAngles;

        // offset distance of SceneView from its actual position
        Vector3 backingDistance = (Quaternion.Euler(rotation) * Vector3.forward).normalized * 2;
        position -= backingDistance;
    
        EditorGUIUtility.systemCopyBuffer = "GenericPropertyJSON:{\"name\":\"camTransform\",\"type\":-1,\"children\":[{\"name\":\"position\",\"type\":9,\"children\":[{\"name\":\"x\",\"type\":2,\"val\":"+ position.x +"},{\"name\":\"y\",\"type\":2,\"val\":"+ position.y +"},{\"name\":\"z\",\"type\":2,\"val\":"+ position.z +"}]},{\"name\":\"rotation\",\"type\":9,\"children\":[{\"name\":\"x\",\"type\":2,\"val\":"+ rotation.x +"},{\"name\":\"y\",\"type\":2,\"val\":"+ rotation.y +"},{\"name\":\"z\",\"type\":2,\"val\":"+ rotation.z +"}]}]}";
    }
}

