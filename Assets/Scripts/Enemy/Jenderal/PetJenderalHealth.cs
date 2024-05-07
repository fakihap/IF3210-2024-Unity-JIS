using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PetJenderalHealth : PetHealth, IDamageable
{
    public GameObject spellEffect;
    public AudioClip deathClip;
    private PetHealerMovement petHealerMovement;
    private PetHealerHeal petHealerHeal;
    private Animator _anim;
    // public Slider healthSlider;
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
        if(CurrStateData.GetCurrentPetHealth() != -1 && currHealth > CurrStateData.GetCurrentPetHealth())
        {
            Debug.Log("Pet healer health reduce");
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
        petHealerMovement.enabled = false;
        petHealerHeal.enabled = false;
        _anim.SetTrigger("Dead");
        spellEffect.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if(isImmortal) return;

        currHealth -= amount;
        // healthSlider.value = currHealth;
        CurrStateData.SetCurrentPetHealth(currHealth);

        if(currHealth <= 0 && !isDead)
        {
            CurrStateData.SetCurrentPetHealth(-1);
            Death();
        }
    }

    public void Disappear()
    {
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