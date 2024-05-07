using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace Nightmare
{
    public class KerocoAttack : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 5;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        bool playerInRange;
        float timer;

        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent<Animator>();

            //StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            // If the exiting collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }

        void Update()
        {
            if (isPaused)
                return;

            timer += Time.deltaTime;

            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                // ... attack.
                AttackPlayer();
            }
            if(timer >= timeBetweenAttacks && PetHealerInRange() && enemyHealth.currentHealth > 0)
            {
                AttackHelaer();
            }
            if(timer >= timeBetweenAttacks && PetAttackerInRange() && enemyHealth.currentHealth > 0)
            {
                AttackAttacker();
            }

            // If the player has zero or less health...
            if (playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger("PlayerDead");
            }
        }

        void AttackPlayer()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerHealth.TakeDamage(attackDamage);
            }
        }
        void AttackHelaer()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            PetHealerHealth petHealerHealth = GameObject.FindGameObjectWithTag("PetHealer").GetComponent<PetHealerHealth>();
            if (petHealerHealth.currHealth > 0)
            {
                // ... damage the player.
                print("attack healer take damage"+ petHealerHealth.currHealth +" "+attackDamage);
                petHealerHealth.TakeDamage(attackDamage);
            }
        }
        void AttackAttacker()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            PetAttackerHealth petAttackerHealth = player.GetComponent<PetAttackerHealth>().GetComponent<PetAttackerHealth>();
            if (petAttackerHealth.currHealth > 0)
            {
                // ... damage the player.
                petAttackerHealth.TakeDamage(attackDamage);
            }
        }
        bool PlayerInRange(){
            return (player.transform.position - transform.position).magnitude <= 1.5;
        }
        bool PetHealerInRange(){
            if(GameObject.FindGameObjectWithTag("PetHealer") == null){
                return false;
            }
            return (GameObject.FindGameObjectWithTag("PetHealer").transform.position - transform.position).magnitude <= 1.5;
        }
        bool PetAttackerInRange()
        {
            if(GameObject.FindGameObjectWithTag("PetAttacker") == null){
                return false;
            }
            return (GameObject.FindGameObjectWithTag("PetAttacker").transform.position - transform.position).magnitude <= 1.5;
        }
    }
}