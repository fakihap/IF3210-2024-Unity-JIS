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
    public float disappearTime = 2.5f;
    public bool isDead;
    public bool isImmortal;
    public bool isDisappear;

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