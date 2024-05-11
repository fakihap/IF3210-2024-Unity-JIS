using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class KepalaKerocoAttack : PausibleObject
    {
        Transform player;

        //attack attribute
        private float attackTimer;
        public float attackTime ;
        public int damage;
        public AudioSource attackSound;
        public Light gunLight;
        public ParticleSystem gunParticles;
        public GameObject gunBarrelEnd;
        public LineRenderer gunLine0;
        public LineRenderer gunLine1;
        public LineRenderer gunLine2;
        public LineRenderer gunLine3;
        public LineRenderer gunLine4;
        public float bulletAngle = 3f;
        
        Ray shootRay;
        RaycastHit shootHit;
        public float range;
        int shootableMask;
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            shootableMask = LayerMask.GetMask("Player");
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void Update()
        {
            if (isPaused)
                return;

            attackTimer += Time.deltaTime;
            if (attackTimer >= attackTime && gameObject.GetComponent<EnemyHealth>().currentHealth > 0)
            {
                print("attack kepalakeroco");
                Attack();
            }
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
                AttackComponent(0);
            }
            else if (distanceToHealer < distanceToAttacker)
            {
                AttackComponent(1);
            }
            else
            {
                AttackComponent(2);
            }
        }

        public void AttackComponent(int type){

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;
            
            if(type==0){
                shootableMask = LayerMask.GetMask("Player");
                print("masuk player");
            }
            else if(type==1){
                shootableMask = LayerMask.GetMask("Shootable");
                print("masuk healer");
            }
            else if(type==2){
                shootableMask = LayerMask.GetMask("Shootable");
                print("masuk attacker");
            }

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                attackTimer = 0f;
                attackSound.Play();

                gunLight.enabled = true;

                gunParticles.Stop();
                gunParticles.Play();

                gunLine0.enabled = true;
                gunLine1.enabled = true;
                gunLine2.enabled = true;
                gunLine3.enabled = true;
                gunLine4.enabled = true;

                gunLine0.SetPosition(0, transform.position);
                gunLine1.SetPosition(0, transform.position);
                gunLine2.SetPosition(0, transform.position);
                gunLine3.SetPosition(0, transform.position);
                gunLine4.SetPosition(0, transform.position);

                print("Player is hit123");

                PlayerHealth playerHealth = null;
                PetHealerHealth petHealerHealth = null;
                PetAttackerHealth petAttackerHealth = null;

                // setting damage
                int maxDamage = damage;
                int minDamage = damage / 2; // Adjust as necessary

                // Calculate the damage based on the distance (closer targets take more damage)
                float distanceToTarget = Vector3.Distance(shootRay.origin, shootHit.point);
                float damageFactor = 1 - (distanceToTarget / range);
                float calculatedDamage = Mathf.Lerp(minDamage, maxDamage, damageFactor);
                

                if(type==0){
                    playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
                    print("ini player health before attack "+ playerHealth.currentHealth);
                    playerHealth.TakeDamage((int)calculatedDamage);
                    print("ini player health after attack "+ playerHealth.currentHealth);
                }
                else if(type==1){
                    if(shootHit.collider.GetComponent<PetHealerHealth>() == null){
                        return;
                    }
                    petHealerHealth = shootHit.collider.GetComponent<PetHealerHealth>();
                    print("ini pet helaer health before attack "+ petHealerHealth.currHealth);
                    petHealerHealth.TakeDamage((int)calculatedDamage);
                    print("ini pet helear health after attack "+ petHealerHealth.currHealth);
                }
                else if(type==2){
                    if(shootHit.collider.GetComponent<PetAttackerHealth>() == null){
                        return;
                    }
                    petAttackerHealth = shootHit.collider.GetComponent<PetAttackerHealth>();
                    print("ini pet attacker health before attack "+ petAttackerHealth.currHealth);
                    petAttackerHealth.TakeDamage((int)calculatedDamage);
                    print("ini pet attacker health after attack "+ petAttackerHealth.currHealth);
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine0.SetPosition(1, shootHit.point);
                gunLine1.SetPosition(1, shootRay.origin + Quaternion.Euler(-bulletAngle, 0f, 0f) * shootRay.direction * range);
                gunLine2.SetPosition(1, shootRay.origin + Quaternion.Euler(bulletAngle, 0f, 0f) * shootRay.direction * range);
                gunLine3.SetPosition(1, shootRay.origin + Quaternion.Euler(0f, bulletAngle, 0f) * shootRay.direction * range);
                gunLine4.SetPosition(1, shootRay.origin + Quaternion.Euler(0f, -bulletAngle, 0f) * shootRay.direction * range);

                Invoke("DisableGunLine", 0.1f);
            }
            else
            {
                print("ga jadi gunggg");
            }
        }

        void DisableGunLine()
        {
            gunLine0.enabled = false;
            gunLine1.enabled = false;
            gunLine2.enabled = false;
            gunLine3.enabled = false;
            gunLine4.enabled = false;
            gunLight.enabled = false;
        }

    }
}