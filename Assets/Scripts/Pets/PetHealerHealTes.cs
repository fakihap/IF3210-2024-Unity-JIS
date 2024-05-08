// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PetWizardHeal : MonoBehaviour
// {
//     public GameObject player;
//     public float timeBetweenHeal = 10f;
//     public int healAmount = 10;
//     float _timer;
//     PlayerHealth playerHealth;
//     bool _playerInRange;
//     AudioSource _healAudio;
//     // Start is called before the first frame update
//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player");
//         playerHealth = player.GetComponent<PlayerHealth>();
//         _healAudio = GetComponent<AudioSource>();
//         _timer = 10f;
//         if ((transform.position - player.transform.position).magnitude <= 6)
//         {
//             _playerInRange = true;
//         }
//         else
//         {
//             _playerInRange = false;
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if ((transform.position - player.transform.position).magnitude <= 6)
//         {
//             _playerInRange = true;
//         }
//         else
//         {
//             _playerInRange = false;
//         }

//         _timer += Time.deltaTime;
//         if (_playerInRange && _timer >= timeBetweenHeal && !PauseManager.CheckIfPaused())
//         {
//             //Debug.Log(_timer);
//             Heal();
//         }
//     }

//     void Heal()
//     {
//         _healAudio.Play();
//         //Debug.Log("Heal");
//         _timer = 0f;
//         playerHealth.TakeHealth(healAmount);
//     }

//     //private void OnTriggerEnter(Collider other)
//     //{
//     //    if (other.gameObject == player)
//     //    {
//     //        _playerInRange = true;
//     //    }
//     //}

//     //private void OnTriggerExit(Collider other)
//     //{
//     //    if (other.gameObject == player)
//     //    {
//     //        _playerInRange = false;
//     //    }
//     //}
// }
