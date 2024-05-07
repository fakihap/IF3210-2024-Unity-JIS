using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

namespace Nightmare
{
    public class OrbIncreaseDamage : MonoBehaviour
    {
        public float despawnTime = 5f;
        public GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            Invoke("Despawn", despawnTime);
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        private void Update()
        {
            if (OrbInrange())
            {
                IncreaseDamage();
                Despawn();
                print("Increase damage via orb");
            }
        }
        bool OrbInrange()
        {
            return (player.transform.position - transform.position).magnitude <= 1.5;
        }

        private void IncreaseDamage()
        {
            PlayerMovement playerMovement= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            playerMovement.AddOrbIncreseDamage();
        }   



        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}