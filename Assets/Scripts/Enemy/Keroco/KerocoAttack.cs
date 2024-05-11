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

        void Update()
        {
            if (isPaused)
                return;


            Attack();

            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }

        void Attack(){
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks && PlayerInRange() && enemyHealth.currentHealth > 0)
            {
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
        }

        void AttackPlayer()
        {
            timer = 0f;
            playerHealth.TakeDamage(attackDamage);
        }
        void AttackHelaer()
        {
            timer = 0f;

            PetHealerHealth petHealerHealth = GameObject.FindGameObjectWithTag("PetHealer").GetComponent<PetHealerHealth>();
            if (petHealerHealth.currHealth > 0)
            {
                petHealerHealth.TakeDamage(attackDamage);
            }
        }
        void AttackAttacker()
        {
            timer = 0f;

            PetAttackerHealth petAttackerHealth = GameObject.FindGameObjectWithTag("PetAttacker").GetComponent<PetAttackerHealth>();
            if (petAttackerHealth.currHealth > 0)
            {
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