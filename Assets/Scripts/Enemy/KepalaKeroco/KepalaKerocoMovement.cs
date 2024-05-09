using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class KepalaKerocoMovement : PausibleObject
    {
        Transform player;
        EnemyHealth enemyHealth;
        NavMeshAgent nav;

        public GameObject enemy;
        private float spawnTimer = 5;
        // private float attackTimer;
        public float spawnTime ;
        // public float attackTime ;
        public float speed = 4f;

        // public AudioSource attackSound;
        // public Light gunLight;
        // public ParticleSystem gunParticles;
        // public GameObject gunBarrelEnd;
        // public LineRenderer gunLine;
        // Ray shootRay;
        // RaycastHit shootHit;
        // public float range;
        // int shootableMask;
        void Awake ()
        {
            enemyHealth = GetComponent<EnemyHealth>();
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            nav = GetComponent<NavMeshAgent>();
            // shootableMask = LayerMask.GetMask("Player");
            nav.speed= speed;
        }


        void Update ()
        {
            if (isPaused)
                return;
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTime)
            {
                print("spawn kepalakeroco");
                Spawn();
                spawnTimer = 0f;
            }

            // attackTimer += Time.deltaTime;
            // if (attackTimer >= attackTime)
            // {
            //     print("attack kepalakeroco");
            //     Attack();
            //     attackTimer = 0f;
            // }

            Transform petHealerMovement = null;
            Transform petAttackerMovement = null;

            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            float distanceToHealer = 999999999;
            float distanceToAttacker = 99999999;
        
            if(GameObject.FindGameObjectWithTag("PetHealer") != null)
            {
                petHealerMovement = GameObject.FindGameObjectWithTag("PetHealer").transform;
                distanceToHealer = Vector3.Distance(petHealerMovement.position, transform.position);
            }
            if(GameObject.FindGameObjectWithTag("PetAttacker") != null)
            {
                petAttackerMovement = GameObject.FindGameObjectWithTag("PetAttacker").transform;
                distanceToAttacker = Vector3.Distance(petAttackerMovement.position, transform.position);
            }

            if (distanceToPlayer < distanceToHealer && distanceToPlayer < distanceToAttacker)
            {
                nav.SetDestination(player.position);
            }
            else if (distanceToHealer < distanceToAttacker)
            {
                nav.SetDestination(petHealerMovement.position);
            }
            else
            {
                nav.SetDestination(petAttackerMovement.position);
            }
        }

        void Spawn ()
        {           
            if(enemyHealth.currentHealth <= 0f)
            {
                return;
            }

            Vector3 enemyPosition = enemyHealth.transform.position;

            Quaternion rotation = Quaternion.Euler(0, 0, 0);            
            
            Instantiate (enemy, enemyPosition, rotation);
        }

        // public void Attack()
        // {
        //     Transform petHealerMovement = null;
        //     Transform petAttackerMovement = null;

        //     float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        //     float distanceToHealer = 999999999;
        //     float distanceToAttacker = 99999999;
        
        //     if(GameObject.FindGameObjectWithTag("PetHealer") != null)
        //     {
        //         petHealerMovement = GameObject.FindGameObjectWithTag("PetHealer").transform;
        //         distanceToHealer = Vector3.Distance(petHealerMovement.position, transform.position);
        //     }
        //     if(GameObject.FindGameObjectWithTag("PetAttacker") != null)
        //     {
        //         petAttackerMovement = GameObject.FindGameObjectWithTag("PetAttacker").transform;
        //         distanceToAttacker = Vector3.Distance(petAttackerMovement.position, transform.position);
        //     }
            
        //     if (distanceToPlayer < distanceToHealer && distanceToPlayer < distanceToAttacker)
        //     {
        //         AttackComponent(0);
        //     }
        //     else if (distanceToHealer < distanceToAttacker)
        //     {
        //         AttackComponent(1);
        //     }
        //     else
        //     {
        //         AttackComponent(2);
        //     }
        // }

        // public void AttackComponent(int type){
        //     attackTimer = 0f;
        //     print("Player is hit attack player");
        //     shootRay.origin = gunBarrelEnd.transform.position;
        //     shootRay.direction = gunBarrelEnd.transform.forward;

        //     if(type==0){
        //         shootableMask = LayerMask.GetMask("Player");
        //     }
        //     else if(type==1){
        //         shootableMask = LayerMask.GetMask("Environment");
        //     }
        //     else if(type==2){
        //         shootableMask = LayerMask.GetMask("Environment");
        //     }

        //     if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        //     {
        //         gunLight.enabled = true;

        //         gunParticles.Stop();
        //         gunParticles.Play();

        //         gunLine.enabled = true;
        //         gunLine.SetPosition(0, gunBarrelEnd.transform.position);
        //         attackSound.Play();
        //         print("Player is hit123");

        //         PlayerHealth playerHealth = null;
        //         PetHealerHealth petHealerHealth = null;
        //         PetAttackerHealth petAttackerHealth = null;
        //         if(type==0){
        //             playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
        //             print("ini player health before attack "+ playerHealth.currentHealth);
        //             playerHealth.TakeDamage(10);
        //             print("ini player health after attack "+ playerHealth.currentHealth);
        //         }
        //         else if(type==1){
        //             petHealerHealth = shootHit.collider.GetComponent<PetHealerHealth>();
        //             print("ini pet helaer health before attack "+ petHealerHealth.currHealth);
        //             petHealerHealth.TakeDamage(10);
        //             print("ini pet helear health after attack "+ petHealerHealth.currHealth);
        //         }
        //         else if(type==2){
        //             petAttackerHealth = shootHit.collider.GetComponent<PetAttackerHealth>();
        //             print("ini pet attacker health before attack "+ petAttackerHealth.currHealth);
        //             petAttackerHealth.TakeDamage(10);
        //             print("ini pet attacker health after attack "+ petAttackerHealth.currHealth);
        //         }

        //         // Set the second position of the line renderer to the point the raycast hit.
        //         gunLine.SetPosition(1, shootHit.point);
        //         Invoke("DisableGunLine", 0.1f);
        //     }
        //     // If the raycast didn't hit anything on the shootable layer...
        //     else
        //     {
        //         // attackSound.Play();
        //         // // ... set the second position of the line renderer to the fullest extent of the gun's range.
        //         // gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        //         print("ga jadi gunggg");
        //     }
        //     // make it delay 0.5s
        // }

        // void DisableGunLine()
        // {
        //     gunLine.enabled = false;
        // }
    }
}