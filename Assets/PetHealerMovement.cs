using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetHealerMovement : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent nav;
    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if ((player.transform.position - transform.position).magnitude >= 0.5f)
        {
            _anim.SetBool("IsMoving", true);
            Debug.Log("Move to player");
            nav.SetDestination(player.transform.position);
            Debug.Log(player.transform.position);

            // Calculate direction to the player
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            directionToPlayer.y = 0f; // Ensure the pet doesn't tilt upwards or downwards

            // Rotate the pet to face the player
            if (directionToPlayer != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
        else
        {
            _anim.SetBool("IsMoving", false);
            nav.ResetPath();
        }
    }
}
