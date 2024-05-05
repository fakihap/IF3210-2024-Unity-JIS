using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetAttackerHealth : PetHealth, IDamageable
{
    public float sinkSpeed = 2.5f;
    public AudioClip deathClip;
    private PetAttackerMovement petAttackerMovement;
    private PetAttackerAttack petAttackerAttack;
    private Animator _anim;
    private bool isDead;
    private bool isImmortal;

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

    public void Immortal()
    {
        isImmortal = true;
    }
}