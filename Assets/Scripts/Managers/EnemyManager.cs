using UnityEngine;

namespace Nightmare
{
    public class EnemyManager : PausibleObject
    {
        private PlayerHealth playerHealth;
        public GameObject enemy;
        public float spawnTime = 3f;
        public Transform[] spawnPoints;

        private float timer;
        private int spawned = 0;

        void Start ()
        {
            timer = spawnTime;
        }

        void OnEnable()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void Update()
        {
            if (isPaused)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Spawn();
                timer = spawnTime;
            }
        }

        void Spawn ()
        {           
            // If the player has no health left...
            if(playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Find a random index between zero and one less than the number of spawn points.

            //get position player
            Vector3 playerPosition = playerHealth.transform.position;

            Quaternion rotation = Quaternion.Euler(0, 0, 0);

            //get random rotation 
            
            
            Instantiate (enemy, playerPosition, rotation);
        }
    }
}