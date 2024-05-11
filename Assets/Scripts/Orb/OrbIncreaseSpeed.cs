using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

namespace Nightmare
{
    public class OrbIncreaseSpeed : MonoBehaviour
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
                IncreaseSpeed();
                Despawn();
                print("increase speed via orb");
            }
        }
        bool OrbInrange()
        {
            return (player.transform.position - transform.position).magnitude <= 1.5;
        }

        private void IncreaseSpeed()
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.OrbIncreaseSpeed(15,1.5f);
        }

        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}