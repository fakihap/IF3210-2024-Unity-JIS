using UnityEngine;

namespace Nightmare
{
    public class RajaAttack : PausibleObject
    {
        private float attackTimer;
        public float attackTime ;
        public float spawnTimer;
        public float dpsTimer;
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 5;
        public int DPSDamage = 2;
        public float rangeDPS = 10f;
        public GameObject enemy;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        float timerDPS;
        float timerSpawn;
        bool isSlow;
        bool isDecreaseDamage;

        // atribut untuk shotgun
        public int shotgunDamage;
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
            isSlow = false;
            isDecreaseDamage = false;
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if (isPaused)
                return;

            if (PlayerInRangeDamage())
            {
                timerDPS -= Time.deltaTime;
                if (timerDPS <= 0f)
                {
                    DPS();
                    timerDPS = dpsTimer;
                }

                timerSpawn -= Time.deltaTime;
                if (timerSpawn <= 0f)
                {
                    Spawn();
                    timerSpawn = spawnTimer;
                }

                if (!isSlow)
                {
                    isSlow = true;
                    SlowPlayer();
                }

                if (!isDecreaseDamage)
                {
                    isDecreaseDamage = true;
                    ChangeDamageDecreseByRaja();
                }
            }
            else
            {
                if (isSlow)
                {
                    DeactivateSlowPlayer();
                    isSlow = false;
                }

                if (isDecreaseDamage)
                {
                    isDecreaseDamage = false;
                    ChangeDamageDecreseByRaja();
                }
            }

            attackTimer += Time.deltaTime;
            if (attackTimer >= attackTime)
            {
                print("attack kepalakeroco");
                Attack();
                attackTimer = 0f;
            }

            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }

        void Attack()
        {
            Transform petHealerMovement = null;
            Transform petAttackerMovement = null;

            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
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
                print("masuk 0 raja");
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
            attackTimer = 0f;

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if(type==0){
                shootableMask = LayerMask.GetMask("Player");
                print("masuk player");
            }
            else if(type==1){
                shootableMask = LayerMask.GetMask("Environment");
                print("masuk healer");
            }
            else if(type==2){
                shootableMask = LayerMask.GetMask("Environment");
                print("masuk attacker");
            }

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
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
                int maxDamage = shotgunDamage;
                int minDamage = shotgunDamage / 2; // Adjust as necessary

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
        }

        bool PlayerInRangeDamage()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance <= rangeDPS;
        }

        void DPS()
        {
            print("ini player healt before dps "+ playerHealth.currentHealth);
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(DPSDamage);
                print("ini player healt after dps "+ playerHealth.currentHealth);
            }
        }

        void SlowPlayer()
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.MultiplierSpeed(0.8f);
        }

        void DeactivateSlowPlayer()
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.MultiplierSpeed(1.2f);
        }

        void ChangeDamageDecreseByRaja()
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.ChangeDamageDecreaseByRaja();
        }

        void Spawn()
        {
            Vector3 enemyPosition = transform.position;
            Quaternion rotation = Quaternion.identity;
            Instantiate(enemy, enemyPosition, rotation);
        }
        public void DoubleDamage()
        {
            attackDamage *= 2;
            DPSDamage *= 2;
        }
        public void ResetDamage()
        {
            attackDamage /= 2;
            DPSDamage /= 2;
        }
    }
}
