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
        isEnemy = true;
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
        print("matii kau petttt");
        isDead = true;
        petBuffMovement.enabled = false;
        GameObject parent = transform.parent.gameObject;
        if(parent.GetComponent<JenderalAttack>() != null){
            parent.GetComponent<JenderalAttack>().ResetDamage();
        }
        else if(parent.GetComponent<RajaAttack>() != null){
            parent.GetComponent<RajaAttack>().ResetDamage();
        }
        HealthBar healthBar = GetComponent<HealthBar>();
        // Destroy(healthBar);
        healthBar.DisableHealthBar();

        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
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
        // if (manager != null)
        // {
        //     manager.SpawnNextPet(transform);
        // }
        print("pet buff disappear");
        Destroy(gameObject, 2f);
    }

    public void Immortal()
    {
        isImmortal = true;
    }
}