using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    //  NOTE THAT LevelManager is a template script
        private string lastScene;
        private GameObject player;
        public static StageManager Instance;
        public void Awake() {
            // Instance 
            if (Instance != null) {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void Start()
        {
            
        }


        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            // teleporters
            List<SceneTeleporter> teleporters = FindObjectsOfType<SceneTeleporter>().ToList();

            // set player pos
            foreach (SceneTeleporter sceneTeleporter in teleporters) {
                if (sceneTeleporter.GetTargetSceneName().Equals(lastScene)) {
                    player.transform.position = sceneTeleporter.spawner.position;
                    player.transform.rotation = sceneTeleporter.spawner.rotation;

                    break; // ensure only one
                }
            }

            SetLastScene(SceneManager.GetActiveScene().name);
        }


        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void SetLastScene(string sceneName) {
            lastScene = sceneName;
        }
}