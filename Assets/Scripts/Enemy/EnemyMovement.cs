using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    // PlayerHealth playerHealth;
    // EnemyHealth enemyHealth;
    NavMeshAgent nav;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // playerHealth = player.GetComponent<PlayerHealth>();
        // enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();

        // StartPausible();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
    }
}
