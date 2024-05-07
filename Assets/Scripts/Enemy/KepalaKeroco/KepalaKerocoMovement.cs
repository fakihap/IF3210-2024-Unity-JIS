using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class KepalaKerocoMovement : PausibleObject
    {
        Transform player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        NavMeshAgent nav;

        public GameObject enemy;
        private float spawnTimer = 5;
        private float attackTimer;
        public float spawnTime ;
        public float attackTime ;
        public float speed = 4f;

        public AudioSource attackSound;
        public Light gunLight;
        public ParticleSystem gunParticles;
        public GameObject gunBarrelEnd;
        public LineRenderer gunLine;
        Ray shootRay;
        RaycastHit shootHit;
        public float range;
        int shootableMask;
        
        float timer;
        void Awake ()
        {
            enemyHealth = GetComponent<EnemyHealth>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            nav = GetComponent<NavMeshAgent>();
            shootableMask = LayerMask.GetMask("Player");
            nav.speed= speed;
        }


        void Update ()
        {
            if (isPaused)
                return;
            // float deltaTime = Time.deltaTime;
            spawnTimer += Time.deltaTime;
            // spawnTimer %= 60;
            // print("spawn timer"+spawnTimer);
            if (spawnTimer >= spawnTime)
            {
                print("spawn kepalakeroco");
                Spawn();
                spawnTimer = 0f;
            }

            attackTimer += Time.deltaTime;
            if (attackTimer >= attackTime)
            {
                print("attack kepalakeroco");
                Attack();
                attackTimer = 0f;
            }

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

            // print("distance to player: " + distanceToPlayer);
            // print("distance to healer: " + distanceToHealer);
            // print("distance to attacker: " + distanceToAttacker);
            
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

        public void Attack()
        {
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
                AttackPlayer();
            }
            else if (distanceToHealer < distanceToAttacker)
            {
                print("Healer is hit 242");
                AttackHealer();
            }
            else
            {
                AttackAttacker();
            }
        }

        public void AttackPlayer()
        {
            attackTimer = 0f;
            print("Player is hit attack player");
            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = gunBarrelEnd.transform.position;
            shootRay.direction = gunBarrelEnd.transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                gunLight.enabled = true;

                // Stop the particles from playing if they were, then start the particles.
                gunParticles.Stop();
                gunParticles.Play();

                // Enable the line renderer and set it's first position to be the end of the gun.
                gunLine.enabled = true;
                gunLine.SetPosition(0, gunBarrelEnd.transform.position);
                attackSound.Play();
                print("Player is hit123");
                // Try and find an EnemyHealth script on the gameobject hit.
                PlayerHealth playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
                print("ini player health"+playerHealth);
                // If the playerHealth component exist...
                print("this is playerrr health "+playerHealth.currentHealth);
                if (playerHealth != null)
                {
                    // ... the enemy should take damage.
                    print("playerrr is take damage");
                    playerHealth.TakeDamage(10);
                }
                else
                {
                    print("Enemy is not take damage");
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition(1, shootHit.point);
                Invoke("DisableGunLine", 0.1f);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // attackSound.Play();
                // // ... set the second position of the line renderer to the fullest extent of the gun's range.
                // gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                print("ga jadi gunggg");
            }
            // make it delay 0.5s
            
        }
        public void AttackHealer()
        {
            attackTimer = 0f;

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = gunBarrelEnd.transform.position;
            shootRay.direction = gunBarrelEnd.transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, LayerMask.GetMask("Environment")))
            {
                gunLight.enabled = true;

                // Stop the particles from playing if they were, then start the particles.
                gunParticles.Stop();
                gunParticles.Play();

                // Enable the line renderer and set it's first position to be the end of the gun.
                gunLine.enabled = true;
                gunLine.SetPosition(0, gunBarrelEnd.transform.position);
                attackSound.Play();
                print("Healer is hit123");
                // Try and find an EnemyHealth script on the gameobject hit.
                PetHealerHealth playerHealth = shootHit.collider.GetComponent<PetHealerHealth>();
                print("ini player health"+playerHealth);
                // If the playerHealth component exist...
                print("this is helaer health "+playerHealth.currHealth);
                if (playerHealth != null)
                {
                    // ... the enemy should take damage.
                    print("playerrr is take damage");
                    playerHealth.TakeDamage(10);
                }
                else
                {
                    print("Enemy is not take damage");
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition(1, shootHit.point);
                Invoke("DisableGunLine", 0.1f);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // attackSound.Play();
                // // ... set the second position of the line renderer to the fullest extent of the gun's range.
                // gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                print("ga jadi gunggg");
            }
            // make it delay 0.5s
        }

        public void AttackAttacker()
        {
            attackTimer = 0f;

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = gunBarrelEnd.transform.position;
            shootRay.direction = gunBarrelEnd.transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                gunLight.enabled = true;

                // Stop the particles from playing if they were, then start the particles.
                gunParticles.Stop();
                gunParticles.Play();

                // Enable the line renderer and set it's first position to be the end of the gun.
                gunLine.enabled = true;
                gunLine.SetPosition(0, gunBarrelEnd.transform.position);
                attackSound.Play();
                print("Attacker is hit123");
                // Try and find an EnemyHealth script on the gameobject hit.
                PetAttackerHealth playerHealth = shootHit.collider.GetComponent<PetAttackerHealth>();
                print("ini player health"+playerHealth);
                // If the playerHealth component exist...
                print("this is attacker health "+playerHealth.currHealth);
                if (playerHealth != null)
                {
                    // ... the enemy should take damage.
                    print("playerrr is take damage");
                    playerHealth.TakeDamage(10);
                }
                else
                {
                    print("Enemy is not take damage");
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition(1, shootHit.point);
                Invoke("DisableGunLine", 0.1f);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // attackSound.Play();
                // // ... set the second position of the line renderer to the fullest extent of the gun's range.
                // gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                print("ga jadi gunggg");
            }
            // make it delay 0.5s
        }
        void DisableGunLine()
        {
            gunLine.enabled = false;
        }
    }
}