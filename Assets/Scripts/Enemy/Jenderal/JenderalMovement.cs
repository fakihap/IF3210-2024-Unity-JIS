using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class JenderalMovement : PausibleObject
    {
        Transform player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        NavMeshAgent nav;
        public float speed = 2f;
        Rigidbody enemyRigidbody;


        void Awake ()
        {
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            nav = GetComponent<NavMeshAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();
            nav.speed= speed;
        }


        void Update ()
        {

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