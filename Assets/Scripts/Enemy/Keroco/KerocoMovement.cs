using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class KerocoMovement : PausibleObject
    {
        Transform player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        NavMeshAgent nav;
        public float speed = 6f;
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
            nav.SetDestination(player.position);
        }

    }
}