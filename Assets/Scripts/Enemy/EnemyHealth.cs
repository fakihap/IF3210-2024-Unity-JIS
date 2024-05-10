using Unity.VisualScripting;
using UnityEngine;

namespace Nightmare
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public AudioClip deathClip;
        public int currentHealth;
        public GameObject[] orbs;
        public GameObject petBuffer;
        public GameObject petBufferRaja1;
        public GameObject petBufferRaja2;

        Animator anim;
        AudioSource enemyAudio;
        ParticleSystem hitParticles;
        CapsuleCollider capsuleCollider;
        EnemyMovement enemyMovement;

        public bool isDead;
        bool isSinking;

        [SerializeField] private EliminationQuestNotifier eliminationQuestNotifier;

        void Awake()
        {
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            enemyMovement = GetComponent<EnemyMovement>();

            currentHealth = startingHealth;
            // print("Enemy health is " + currentHealth);

            eliminationQuestNotifier = GetComponent<EliminationQuestNotifier>();
        }

        // private void SetKinematics(bool isKinematic)
        // {
        //     capsuleCollider.isTrigger = isKinematic;
        //     capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        // }

        void Update()
        {
            if (isSinking)
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }

            if (currentHealth <= 0) {
                print("ini nama agme object "+gameObject.name);
                if(gameObject.name=="Jenderal(Clone)") {
                    print("masuk jenderal pabskuyt");
                    PetBuffHealth petBuffHealth = GameObject.FindGameObjectWithTag("PetBuffer").GetComponent<PetBuffHealth>();
                    print("ini darah pet buff "+ petBuffHealth.currHealth);
                }
            }
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            print("damage sebeasr "+amount);
            if (isDead)
            {
                return;
            }
            // print("Enemy is take damage");

            enemyAudio.Play();

            currentHealth -= amount;

            hitParticles.transform.position = hitPoint;
            hitParticles.Play();

            if (currentHealth <= 0)
            {
                CurrStateData.currGameData.enemyKilled += 1;
                int savedEnemyKilled = PlayerPrefs.GetInt("enemyKilled");
                // print(savedEnemyKilled);
                PlayerPrefs.SetInt("enemyKilled", savedEnemyKilled + 1);
                Death();
            }
        }

        void Death()
        {
            isDead = true;

            capsuleCollider.isTrigger = true;
            anim.SetTrigger("Dead");
            enemyAudio.clip = deathClip;
            enemyAudio.Play();
            float random = Random.Range(0f, 1f);
            SpawnOrb();
            // set dead
            eliminationQuestNotifier.NotifyElimination();

            // i want to destroy the enemy
            JenderalMovement jenderalMovement = GameObject.FindGameObjectWithTag("Enemy").GetComponent<JenderalMovement>();
            // if(!jenderalMovement.IsUnityNull()) {
            //     print("Jenderal is not null");
            // }
            StartSinking();
            print("this is enemy namaaa "+ gameObject.name);
            if(gameObject.GetType().Name == "Jenderal") {
                print("masuk jenderall lalalala");
            }
            // Destroy(gameObject, 2f);
        }

        public void SpawnOrb()
        {
            print("Spawn orb");
            float random = Random.Range(0f, 1f);
            if(random>0.5f)
            {
                return;
            }
            PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            if(playerMovement.OrbIncreaseDamageCount >= 15) {
                return;
            }
            Vector3 enemyPosition = transform.position;
            enemyPosition.y +=0.5f;
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            GameObject orb = orbs[Random.Range(0, orbs.Length)];
            Instantiate(orb, enemyPosition, rotation);
        }

        public void StartSinking()
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;

            isSinking = true;

            if(gameObject.name=="Keroco(Clone)"){
                CurrStateData.AddCoin(100);
            } else if(gameObject.name=="KepalaKeroco(Clone)") {
                CurrStateData.AddCoin(150);
            } else if(gameObject.name=="Jenderal(Clone)") {
                CurrStateData.AddCoin(200);
                petBuffer.GetComponent<PetBuffHealth>().TakeDamage(100);
            }
            else if(gameObject.name=="Raja"){
                petBufferRaja1.GetComponent<PetBuffHealth>().TakeDamage(100);
                petBufferRaja2.GetComponent<PetBuffHealth>().TakeDamage(100);
                // if(!petBufferRaja1.IsUnityNull()){
                // }
                // if(!petBufferRaja2.IsUnityNull()){
                // }
            }

            Destroy(gameObject, 1f);

            ScoreManager.score += scoreValue;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void AddHealth(int amount)
        {
            currentHealth += amount;
        }

    }
}