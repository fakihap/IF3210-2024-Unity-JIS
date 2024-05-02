using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetAttackerAttack : MonoBehaviour
{
    public GameObject projectile;
    NavMeshAgent nav;
    Animator _anim;
    Rigidbody petAttackerRigidbody;
    PetAttackerMovement petAttackerMovement;
    float fireballDelay = 0.1f;
    float time;
    RaycastHit hit;
    public float radius = 5f;
    private AudioSource fireballAudio;
    int shootableMask;

    // Start is called before the first frame update
    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        time = fireballDelay;
        nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        petAttackerMovement = GetComponent<PetAttackerMovement>();
        petAttackerRigidbody = GetComponent<Rigidbody>();
        fireballAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= fireballDelay)
        {
            LaunchFireball();
            Debug.Log("Panggil Fireball");
        }
    }

    void LaunchFireball()
    {
        if(petAttackerMovement.closestDamageable != null)
        {
            Debug.Log("TIDAK NULL");
            var enemyTransform = petAttackerMovement.closestDamageable.transform;
            var enemyPosition = new Vector3(enemyTransform.position.x, 0, enemyTransform.position.z);
            var petAttackerPosition = new Vector3(transform.position.x, 0, transform.position.z);
            if(Vector3.Distance(petAttackerPosition, enemyPosition) < radius)
            {
                fireballAudio.Play();
                if(HasLineofSightTo(enemyTransform))
                {
                    _anim.SetTrigger("Attack");
                    nav.enabled = false;
                    time = 0;
                    GameObject fireball = Instantiate(projectile,
                        transform.position + (transform.rotation * (new Vector3(0, 1, 1.5f))),
                        transform.rotation * Quaternion.Euler(-90, 0, 0)
                    );
                }
                else
                {
                    _anim.SetTrigger("Attack");
                    nav.enabled = false;
                    time = 0;
                    LookAt(petAttackerMovement.closestDamageable.transform);
                    GameObject fireball = Instantiate(projectile,
                        transform.position + (transform.rotation * (new Vector3(0, 1, 1.5f))),
                        transform.rotation * Quaternion.Euler(-90, 0, 0)
                    );
                }
            }
            else
            {
                nav.enabled =  true;
            }
        }
        else
        {
            nav.enabled = true;
        }
    }

    void LookAt(Transform dest)
    {
        var destVector = new Vector3(dest.position.x, 0, dest.position.z);
        var srcVector = new Vector3(transform.position.x, 0, transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(destVector - srcVector);
        petAttackerRigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, 0.4f));
    }

    bool HasLineofSightTo(Transform dest)
    {
        var enemyPosition = new Vector3(dest.position.x, 0, dest.position.z);
        var fireballPosition = new Vector3(transform.position.x, 0, transform.position.z);
        if(Physics.SphereCast(fireballPosition, 1.05f, (enemyPosition-fireballPosition).normalized, out hit, radius, shootableMask))
        {
            EnemyHealth damageable;
            if(hit.collider.TryGetComponent<EnemyHealth>(out damageable))
            {
                return damageable.transform == dest;
            }
        }

        return false;
    }
}