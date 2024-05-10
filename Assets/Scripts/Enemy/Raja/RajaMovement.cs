using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class RajaMovement : PausibleObject
    {
        EnemyHealth enemyHealth;
        public GameObject enemy;
        public float spawnTime ;
        private float spawnTimer;
        Transform player;
        NavMeshAgent nav;
        public float speed = 6f;


        void Awake ()
        {
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            nav = GetComponent<NavMeshAgent>();
            nav.speed= speed;
        }

        void Spawn()
        {
            if(GetComponent<EnemyHealth>().currentHealth<=0f)
            {
                return;
            }

            Vector3 enemyPosition = GetComponent<EnemyHealth>().transform.position;
            Quaternion rotation = Quaternion.identity;
            Instantiate(enemy, enemyPosition, rotation);
        }


        void Update ()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTime)
            {
                print("spawn kepalakeroco");
                Spawn();
                spawnTimer = 0f;
            }

            Transform petHealerMovement = null;
            Transform petAttackerMovement = null;

            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            float distanceToHealer = 999999999;
            float distanceToAttacker = 99999999;
        
            if(GameObject.FindGameObjectWithTag("PetHealer") != null)
            {
                petHealerMovement = GameObject.FindGameObjectWithTag("PetHealer").transform;
                distanceToHealer = Vector3.Distance(petHealerMovement.position, transform.position);
            }
            if(GameObject.FindGameObjectWithTag("PetAttacker") != null)
            {
                petAttackerMovement = GameObject.FindGameObjectWithTag("PetAttacker").transform;
                distanceToAttacker = Vector3.Distance(petAttackerMovement.position, transform.position);
            }
            

            // If player is closest, attack player
            if (distanceToPlayer < distanceToHealer && distanceToPlayer < distanceToAttacker)
            {
                nav.SetDestination(player.position);
            }
            // attack healer if closer than pet attacker
            else if (distanceToHealer < distanceToAttacker)
            {
                nav.SetDestination(petHealerMovement.position);
            }
            // attack healer if closer than pet attacker
            else
            {
                nav.SetDestination(petAttackerMovement.position);
            }
        }

    }
}