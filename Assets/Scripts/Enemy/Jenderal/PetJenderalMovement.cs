using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetJenderalMovement : MonoBehaviour
{
    public GameObject jenderal;
    NavMeshAgent nav;
    Animator _anim;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        jenderal = transform.parent.gameObject;
        nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //heal player
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            EnemyHealth heal = jenderal.GetComponent<EnemyHealth>();
            heal.AddHealth(10);
            print("Healing player");
            timer = 5f;
        }
    }

    private void FixedUpdate()
    {
        if ((jenderal.transform.position - transform.position).magnitude >= 4.5)
        {
            _anim.SetBool("IsMoving", true);
            // Debug.Log("Move to player");
            nav.SetDestination(jenderal.transform.position);
            // Debug.Log(player.transform.position);

            // Calculate direction to the player
            Vector3 directionToPlayer = (jenderal.transform.position - transform.position).normalized;
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
