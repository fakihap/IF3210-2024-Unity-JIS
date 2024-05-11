using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PetAttackerHealth : PetHealth, IDamageable
{
    public AudioClip deathClip;
    private PetAttackerMovement petAttackerMovement;
    private PetAttackerAttack petAttackerAttack;
    private Animator _anim;
    public Slider healthSlider;
    public float disappearTime = 2.5f;
    private bool isDead;
    private bool isDisappear;

    private void Awake()
    {
        isDead = false;
        isImmortal = false;
        currHealth = startHealth;
        healthSlider = GameObject.FindGameObjectWithTag("SliderPet").GetComponent<Slider>();
        healthSlider.value = currHealth;
        CurrStateData.SetCurrentPetHealth(currHealth);
        _anim = GetComponent<Animator>();
        petAttackerMovement = GetComponent<PetAttackerMovement>();
        petAttackerAttack = GetComponent<PetAttackerAttack>();
        isEnemy = false;
    }

    private void Update()
    {
        /* TO DO: use state data */
        // Debug.Log("Take Damage Pet Attacker");
        if(CurrStateData.GetCurrentPetHealth() != -1 && currHealth > CurrStateData.GetCurrentPetHealth())
        {
            Debug.Log("Pet attacker health reduce");
            TakeDamage(startHealth - CurrStateData.GetCurrentPetHealth());
        }
        
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
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Disappear();
    }

    public void TakeDamage(int amount)
    {
        if(isImmortal) return;

        currHealth -= amount;
        healthSlider.value = currHealth;
        CurrStateData.SetCurrentPetHealth(currHealth);

        if(currHealth <= 0 && !isDead)
        {
            CurrStateData.SetCurrentPetHealth(-1);
            Death();
        }
    }

    public void Disappear()
    {
        CurrStateData.RemoveCurrentPet();
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isDisappear = true;
        if (manager != null)
        {
            manager.SpawnNextPet(transform);

        }
        Destroy(gameObject, 2f);
    }

    public void Immortal()
    {
        isImmortal = true;
    }
}