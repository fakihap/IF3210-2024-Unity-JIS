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
        private float spawnTimer;
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

            spawnTimer += Time.deltaTime;
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
            nav.SetDestination(player.position);
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
            spawnTimer = 0f;
            attackTimer = 0f;

            

            gunLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop();
            gunParticles.Play();

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;
            gunLine.SetPosition(0, gunBarrelEnd.transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = gunBarrelEnd.transform.position;
            shootRay.direction = gunBarrelEnd.transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                attackSound.Play();
                print("Player is hit123");
                // Try and find an EnemyHealth script on the gameobject hit.
                PlayerHealth playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
                print(playerHealth);
                // If the playerHealth component exist...
                print("this is playerrr health "+playerHealth);
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
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                attackSound.Play();
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                print("ga jadi gunggg");
            }
            // make it delay 0.5s
            Invoke("DisableGunLine", 0.1f);
        }
        void DisableGunLine()
        {
            gunLine.enabled = false;
        }
    }
}