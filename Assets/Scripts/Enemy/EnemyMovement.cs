using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public class EnemyMovement : PausibleObject
    {
        Transform player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        NavMeshAgent nav;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
        EnemyHealth enemyHealth;
            nav = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                nav.enabled = false;
            }
        }

        public void GoToPlayer()
        {

        }
                }
            }
        }

        private void IsPsychic()
        {
            GoToPlayer();
        }

        private Vector3 GetRandomPoint(float distance, int layermask)
        {
            Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * distance + this.transform.position;;

            NavMeshHit navHit;
            NavMesh.SamplePosition(randomPoint, out navHit, distance, layermask);

            return navHit.position;
        }

        public void ScaleVision(float scale)
        {
            currentVision = visionRange * scale;
        }

        private int GetCurrentNavArea()
        {
            NavMeshHit navHit;
            nav.SamplePathPosition(-1, 0.0f, out navHit);

            return navHit.mask;
        }

        //void OnDrawGizmos()
        //{
        //    Vector3 position = this.transform.position;
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(position, currentVision);
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawWireSphere(position, hearingRange);
        //}
    }
}
