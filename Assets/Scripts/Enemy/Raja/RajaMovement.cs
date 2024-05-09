using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class RajaMovement : PausibleObject
    {
        Transform player;
        NavMeshAgent nav;
        public float speed = 6f;


        void Awake ()
        {
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            nav = GetComponent<NavMeshAgent>();
            nav.speed= speed;
        }


        void Update ()
        {
            nav.SetDestination(player.position);
        }

    }
}