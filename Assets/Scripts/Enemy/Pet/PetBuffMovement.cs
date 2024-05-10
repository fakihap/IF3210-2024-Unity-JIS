using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetBuffMovement : MonoBehaviour
{
    GameObject enemy;
    NavMeshAgent nav;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //heal player
    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = (player.transform.position - enemy.transform.position).normalized;
        directionToPlayer.y = 0f; // Ensure the pet doesn't tilt upwards or downwards

        // Rotate the pet to face away from the player
        Quaternion lookRotation = Quaternion.LookRotation(-directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        // Move the pet away from the player along the enemy's path
        Vector3 destination = enemy.transform.position + (enemy.transform.position - player.transform.position).normalized * 4.5f;
        nav.SetDestination(destination);
    }
}
