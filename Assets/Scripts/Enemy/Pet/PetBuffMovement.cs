using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetBuffMovement : MonoBehaviour
{
    GameObject enemy;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //heal player
    }

    private void FixedUpdate()
    {
        if ((enemy.transform.position - transform.position).magnitude >= 4.5)
        {

            // Debug.Log("Move to player");
            nav.SetDestination(enemy.transform.position);
            // Debug.Log(player.transform.position);

            // Calculate direction to the player
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
            directionToEnemy.y = 0f; // Ensure the pet doesn't tilt upwards or downwards

            // Rotate the pet to face the Enemy
            if (directionToEnemy != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
        else
        {

            nav.ResetPath();
        }
    }
}
