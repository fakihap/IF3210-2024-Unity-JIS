using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PetBuffHealth : PetHealth, IDamageable
{
    private PetBuffMovement petBuffMovement;
    // public Slider healthSlider;
    public float disappearTime = 2.5f;
    public bool isDead;
    public bool isDisappear;

    private void Awake()
    {
        isDead = false;
        isImmortal = false;
        currHealth = startHealth;
        petBuffMovement = GetComponent<PetBuffMovement>();
    }

    private void Update()
    {
        /* TO DO: use state data */
        // if(CurrStateData.GetCurrentPetHealth() != -1 && currHealth > CurrStateData.GetCurrentPetHealth())
        // {
        //     TakeDamage(startHealth - CurrStateData.GetCurrentPetHealth());
        // }

        if(isDisappear)
        {
            transform.Translate(Vector3.down * (disappearTime * Time.deltaTime));
        }
    }

    private void Death()
    {
        isDead = true;
        petBuffMovement.enabled = false;
    }

    public void TakeDamage(int amount)
    {
        print("pet buff take damage " + amount);
        if(isImmortal) return;

        currHealth -= amount;
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
        GameObject enemy = transform.parent.gameObject;
        if(enemy.GetComponent<JenderalAttack>() != null)
        {
            enemy.GetComponent<JenderalAttack>().ResetDamage();
        }
        else if(enemy.GetComponent<RajaAttack>() != null)
        {
            enemy.GetComponent<RajaAttack>().ResetDamage();
        }
        Destroy(gameObject, 2f);
    }

    public void Immortal()
    {
        isImmortal = true;
    }
}