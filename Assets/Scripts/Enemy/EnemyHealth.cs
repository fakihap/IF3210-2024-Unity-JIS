﻿using UnityEngine;

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

        Animator anim;
        AudioSource enemyAudio;
        ParticleSystem hitParticles;
        CapsuleCollider capsuleCollider;
        EnemyMovement enemyMovement;

        public bool isDead;
        bool isSinking;

        [SerializeField] private DefeatQuestNotifier defeatQuestNotifier;

        void Awake()
        {
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            enemyMovement = GetComponent<EnemyMovement>();

            currentHealth = startingHealth;
            // print("Enemy health is " + currentHealth);

            defeatQuestNotifier = GetComponent<DefeatQuestNotifier>();
        }

        private void SetKinematics(bool isKinematic)
        {
            capsuleCollider.isTrigger = isKinematic;
            capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        }

        void Update()
        {
            if (isSinking)
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
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
            defeatQuestNotifier.NotifyDefeat();

            //buat peluang 0-1 dalam float
            // if(random>0.5f)
            // {
            //     SpawnOrb();
            // }
        }

        public void SpawnOrb()
        {
            print("Spawn orb");
            float random = Random.Range(0f, 1f);
            // if(random>0.5f)
            // {
            //     return;
            // }
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

            Destroy(gameObject, 2f);

            ScoreManager.score += scoreValue;
        }

        public bool IsDead()
        {
            return isDead;
        }

    }
}