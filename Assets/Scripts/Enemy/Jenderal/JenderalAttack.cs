using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class JenderalAttack : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 5;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        bool playerInRange;
        float timer;
        float timerDPS;

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

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            if(playerInRangeDamage())
            {
                timerDPS -= Time.deltaTime;
                if (timerDPS <= 0f)
                {
                    DPS();
                    timerDPS = 5f;
                }
            }

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                // ... attack.
                Attack();
            }

            // If the player has zero or less health...
            if (playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger("PlayerDead");
            }
        }

        void Attack()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                print("attack");
                // ... damage the player.
                playerHealth.TakeDamage(attackDamage);
            }
        }

        bool playerInRangeDamage(){
            if(player.transform.position.x - transform.position.x < 10 && player.transform.position.z - transform.position.z < 10){
                return true;
            }
            return false;
        }

        void DPS(){
            if (playerHealth.currentHealth > 0)
            {
                print("attack DPS");
                playerHealth.TakeDamage(4);
            }
        }
    }
}