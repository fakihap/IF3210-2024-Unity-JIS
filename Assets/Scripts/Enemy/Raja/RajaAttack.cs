using UnityEngine;

namespace Nightmare
{
    public class RajaAttack : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 5;
        public int DPSDamage = 2;
        public float rangeDPS = 10f;
        public GameObject enemy;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        bool playerInRange;
        float timer;
        float timerDPS;
        float timerSpawn;
        bool isSlow;
        bool isDecreaseDamage;

        void Awake()
        {
            isSlow = false;
            isDecreaseDamage = false;
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if (isPaused)
                return;

            timer += Time.deltaTime;

            if (PlayerInRangeDamage())
            {
                timerDPS -= Time.deltaTime;
                if (timerDPS <= 0f)
                {
                    DPS();
                    timerDPS = 5f;
                }

                timerSpawn -= Time.deltaTime;
                if (timerSpawn <= 0f)
                {
                    Spawn();
                    timerSpawn = 5f;
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

            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                Attack();
            }

            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }

        void Attack()
        {
            timer = 0f;
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        bool PlayerInRangeDamage()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance <= rangeDPS;
        }

        void DPS()
        {
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(DPSDamage);
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

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
            }
        }
    }
}
