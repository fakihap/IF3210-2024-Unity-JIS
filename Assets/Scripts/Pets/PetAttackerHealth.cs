using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetAttackerHealth : PetHealth, IDamageable
{
    public AudioClip deathClip;
    private PetAttackerMovement petAttackerMovement;
    private PetAttackerAttack petAttackerAttack;
    private Animator _anim;
    public float disappearTime = 2.5f;
    private bool isDead;
    private bool isImmortal;
    private bool isDisappear;

    private void Awake()
    {
        isDead = false;
        isImmortal = false;
        currHealth = startHealth;
        _anim = GetComponent<Animator>();
        petAttackerMovement = GetComponent<PetAttackerMovement>();
        petAttackerAttack = GetComponent<PetAttackerAttack>();
    }

    private void Update()
    {
        /* TO DO: use state data */
        Debug.Log("Take Damage Pet Attacker");
        
        if(isDisappear)
        {
            transform.Translate(Vector3.down * (disappearTime * Time.deltaTime));
        }
    }

    private void Death()
    {
        isDead = true;
        petAttackerMovement.enabled = false;
        petAttackerAttack.enabled = false;
        _anim.SetTrigger("Dead");
    }

    public void TakeDamage(int amount)
    {
        if(!isImmortal)
        {
            currHealth -= amount;
            /* TO DO: set currHealthData */
        }

        if(currHealth <= 0 && !isDead)
        {
            /* TO DO:  */
            Death();
        }
    }

    public void Disappear()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isDisappear = true;

        Destroy(gameObject, 2f);
    }

    public void Immortal()
    {
        isImmortal = true;
    }
}