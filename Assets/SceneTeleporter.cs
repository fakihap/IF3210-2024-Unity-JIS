using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] string targetSceneName = "Stage01";
    public Transform spawner;
    private void OnTriggerEnter(Collider other)
    {
        print("Teleporter collides with: " + other.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    public string GetTargetSceneName() {
        return targetSceneName;
    }
}
