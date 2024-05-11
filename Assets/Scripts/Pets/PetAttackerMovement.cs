using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetAttackerMovement : MonoBehaviour
{
    NavMeshAgent nav;
    Animator _anim;
    PetAttackerAttack petAttackerAttack;
    bool move = false;
    Rigidbody petAttackRigidbody;
    public List<EnemyHealth> Damageables = new List<EnemyHealth>();
    public EnemyHealth closestDamageable;
    public float thresholdToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        petAttackerAttack = GetComponent<PetAttackerAttack>();
        petAttackRigidbody = GetComponent<Rigidbody>();
    }

    void LookAt(Transform dest)
    {
        var destVector = new Vector3(dest.position.x, 0, dest.position.z);
        var srcVector = new Vector3(transform.position.x, 0, transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(destVector - srcVector);
        petAttackRigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, 0.4f));
    }

    // Update is called once per frame
    void Update()
    {
        move = false;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToPlayer > thresholdToPlayer)
        {
            LookAt(player.transform);
            move = true;
            _anim.SetBool("IsMoving", move);
            nav.SetDestination(player.transform.position);
            return;
        }

        if(Damageables.Count > 0)
        {
            if(closestDamageable == null)
            {
                float closestDistance = float.MaxValue;
                for(int i=0; i<Damageables.Count; i++)
                {
                    var damageable = Damageables[i];
                    if(damageable != null)
                    {
                        var damageableTransform = damageable.transform;
                        float distance = Vector3.Distance(transform.position, damageableTransform.position);
                        if(distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestDamageable = damageable;
                        }
                    }
                }
            }

            if(closestDamageable != null)
            {
                if(nav.enabled)
                {
                    move = true;
                    _anim.SetBool("IsMoving", move);
                    nav.SetDestination(closestDamageable.transform.position);
                }
            }
            else
            {
                _anim.SetBool("IsMoving", move);
            }
        }
        else
        {
            closestDamageable = null;
            _anim.SetBool("IsMoving", move);
        }

        if(closestDamageable == null || closestDamageable.isDead || closestDamageable.currentHealth < 0)
        {
            Damageables.Remove(closestDamageable);
            closestDamageable =   null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (
            other.CompareTag("Enemy")
            )
        {
            var enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Damageables.Add(enemyHealth);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (
            other.CompareTag("Enemy")
            )
        {
            var enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Damageables.Remove(enemyHealth);
            }
        }
    }
}
