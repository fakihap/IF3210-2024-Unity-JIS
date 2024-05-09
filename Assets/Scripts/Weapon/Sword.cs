using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace Nightmare
{
    public class Sword : Weapon
    {
        float timer;
        GameObject player;
        private List<GameObject> enemiesInsideCollider;
        Animator playerAnimator;
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerAnimator = player.GetComponent<Animator>();
            enemiesInsideCollider = new List<GameObject>();
        }

        public override void Attack()
        {
            timer = 0f;

            foreach (GameObject enemy in enemiesInsideCollider)
            {
                if (enemy.Equals(null)) {
                    enemiesInsideCollider.Remove(enemy);
                    continue;
                } 
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                PetBuffHealth petBuffHealth = enemy.GetComponent<PetBuffHealth>();
                if (!enemyHealth.IsUnityNull())
                {
                    if(enemyHealth.currentHealth <= 0){
                        continue;
                    }

                    // ... damage the player.
                    print("darah enemy " + enemyHealth.currentHealth);
                    PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                    int damage = baseDamage+10*playerMovement.OrbIncreaseDamageCount;
                    if(playerMovement.DamageDecreaseByRaja){
                        damage = damage * 80 / 100;
                    }
                    CurrStateData.currGameData.damageDealt += damage;

                    int savedDamageDealt = PlayerPrefs.GetInt("damageDealt");
                    PlayerPrefs.SetInt("damageDealt", savedDamageDealt + damage);     

                    enemyHealth.TakeDamage(damage, new Vector3(0f, 0.5f, 0f));
                    print("darah now enemy " + enemyHealth.currentHealth);
                }
                else if (!petBuffHealth.IsUnityNull())
                {
                    if(petBuffHealth.currHealth <= 0){
                        continue;
                    }
                    // ... damage the pet.
                    print("darah pet " + petBuffHealth.currHealth);
                    PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                    int damage = baseDamage+10*playerMovement.OrbIncreaseDamageCount;
                    if(playerMovement.DamageDecreaseByRaja){
                        damage = damage * 80 / 100;
                    }
                    CurrStateData.currGameData.damageDealt += damage;

                    int savedDamageDealt = PlayerPrefs.GetInt("damageDealt");
                    PlayerPrefs.SetInt("damageDealt", savedDamageDealt + damage);     
                                        
                    petBuffHealth.TakeDamage(damage);
                    print("darah now pet " + petBuffHealth.currHealth);
                }   
            
            }
        }

        public override void UpdateAttack()
        {
            timer += Time.deltaTime;
            // Debug.Log("Number of enemies inside the collider: " + enemiesInsideCollider.Count);

            if (Input.GetButton("Fire1") && timer >= attackSpeed && !PauseManager.IsPaused())
            {
                playerAnimator.SetBool("IsSwordAttack", true);
                if (enemiesInsideCollider.Count > 0)
                {
                    Attack();
                }
                attackSound.Stop();
                attackSound.Play();
            }
            else
            {
                playerAnimator.SetBool("IsSwordAttack", false);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            // Check if the colliding object is an enemy
            if (other.CompareTag("Enemy") && !enemiesInsideCollider.Contains(other.gameObject))
            {
                // Add the enemy to the list
                enemiesInsideCollider.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Check if the colliding object is an enemy
            if (other.CompareTag("Enemy"))
            {
                // Remove the enemy from the list
                enemiesInsideCollider.Remove(other.gameObject);
            }
        }


        public override void DisableEffects()
        {

        }

        public override void IncreaseDamage(int damageIncrease)
        {
            baseDamage += baseDamage * damageIncrease / 100;
        }

    }

}
