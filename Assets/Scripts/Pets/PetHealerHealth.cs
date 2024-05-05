using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetHealerHealth : PetHealth, IDamageable
{
    public GameObject spellEffect;
    public AudioClip deathClip;
    private PetHealerMovement petHealerMovement;
    private PetHealerHeal petHealerHeal;
    private Animator _anim;
    public bool isDead;
    public bool isImmortal;

    private void Awake()
    {
        isDead = false;
        isImmortal = false;
        currHealth = startHealth;
        _anim = GetComponent<Animator>();
        petHealerMovement = GetComponent<PetHealerMovement>();
        petHealerHeal = GetComponent<PetHealerHeal>();
    }

    private void Update()
    {
        /* TO DO: use state data */
        Debug.Log("Take Damage Pet Healer");
    }

    private void Death()
    {
        isDead = true;
        petHealerMovement.enabled = false;
        petHealerHeal.enabled = false;
        _anim.SetTrigger("Dead");
        spellEffect.SetActive(false);
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