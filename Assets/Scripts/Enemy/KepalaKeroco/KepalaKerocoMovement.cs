using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class KepalaKerocoMovement : PausibleObject
    {
        Transform player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        NavMeshAgent nav;

        public GameObject enemy;
        private float timer;
        public float spawnTime = 3f;
        public float speed = 4f;
        void Awake ()
        {
            enemyHealth = GetComponent<EnemyHealth>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            nav = GetComponent<NavMeshAgent>();
            nav.speed= speed;
        }


        void Update ()
        {
            if (isPaused)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Spawn();
                timer = spawnTime;
            }
            nav.SetDestination(player.position);
        }

        void Spawn ()
        {           
            if(enemyHealth.currentHealth <= 0f)
            {
                return;
            }

            Vector3 enemyPosition = enemyHealth.transform.position;

            Quaternion rotation = Quaternion.Euler(0, 0, 0);            
            
            Instantiate (enemy, enemyPosition, rotation);
        }

    }
}