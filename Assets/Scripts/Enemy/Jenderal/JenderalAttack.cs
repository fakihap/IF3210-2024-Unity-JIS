using UnityEngine;
using System.Collections;
using System;

namespace Nightmare
{
    public class JenderalAttack : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 5;
        public int DPSDamage = 2;
        public float rangeDPS = 10f;

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

            if(PlayerInRangeDamage())
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

            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        bool PlayerInRangeDamage(){
            float range = (float)Math.Sqrt((player.transform.position.x - transform.position.x)*(player.transform.position.x - transform.position.x) + (player.transform.position.z - transform.position.z)*(player.transform.position.z - transform.position.z));
            if(range <= rangeDPS){
                return true;
            }
            return false;
        }

        void DPS(){
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(DPSDamage);
            }
        }
    }
}