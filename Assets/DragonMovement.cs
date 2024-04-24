using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonMovement : MonoBehaviour
{
    public Transform target;
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
        if ((player.transform.position - transform.position).magnitude >= 0.5)
        {
            _anim.SetBool("IsMoving", true);
            Debug.Log("Move to player");
            nav.SetDestination(player.transform.position);
            Debug.Log(player.transform.position);
        }
        else
        {
            _anim.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {

    }
}
