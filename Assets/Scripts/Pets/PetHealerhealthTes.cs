// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PetWizardHealth : PetHealth, IDamageableFriendly
// {
//     private static readonly int Dead = Animator.StringToHash("Dead");

//     public float sinkSpeed = 2.5f;
//     public GameObject spellEffect;
//     public AudioClip deathClip;
//     private PetWizardMovement _petWizardMovement;
//     private PetWizardHeal _petWizardHeal;
//     private PetWizardAuraBuff _petWizardAuraBuff;
//     private Animator _anim;
//     public bool _isDead;
//     private bool _isSinking;
//     private bool _immortal;

//     private void Awake()
//     {
//         // Mendapatkan reference komponen
//         _isDead = false;
//         _anim = GetComponent<Animator>();
//         _petWizardMovement = GetComponent<PetWizardMovement>();
//         _petWizardHeal = GetComponent<PetWizardHeal>();
//         _petWizardAuraBuff = GetComponent<PetWizardAuraBuff>();
//         currentHealth = startingHealth;
//         _immortal = false;
//     }

//     private void Update()
//     {
//         //if (manager == null)
//         //{
//         //    Debug.Log("Manager == null");
//         //}
//         //else
//         //{
//         //    Debug.Log("Manager =! null");
//         //}

//         if (CurrentStateData.GetCurrentPethealth() != -1 && currentHealth > CurrentStateData.GetCurrentPethealth())
//         {
//             Debug.Log("reduce");
//             TakeDamage(startingHealth - CurrentStateData.GetCurrentPethealth());
//         }

//         if (_isSinking)
//         {
//             // Memindahkan object kebawah
//             transform.Translate(Vector3.down * (sinkSpeed * Time.deltaTime));
//         }
//     }

//     private void Death()
//     {
//         _isDead = true;
//         _petWizardMovement.enabled = false;
//         if (_petWizardHeal != null)
//         {
//             _petWizardHeal.enabled = false;
//         }

//         if (_petWizardAuraBuff != null)
//         {
//             _petWizardAuraBuff.enabled = false;
//         }

//         // Mentrigger animasi Die
//         _anim.SetTrigger(Dead);

//         // Memainkan suara ketika mati
//         //_petWizardAudio.clip = deathClip;
//         //_petWizardAudio.Play();

//         // Mematikan script petWizard movement
        
//         spellEffect.SetActive(false);
//     }

//     // Fungsi untuk mendapatkan damage
//     public void TakeDamage(int amount)
//     {
//         if (!_immortal)
//         {
//             // Mengurangi health
//             currentHealth -= amount;
//             CurrentStateData.SetCurrentPethealth(currentHealth);
//         }

//         // Memainkan suara ketika terkena damage
//         //_petWizardAudio.Play();

//         // Memanggil method Death() jika health <= 0 dan belum mati
//         if (currentHealth <= 0 && !_isDead)
//         {
//             CurrentStateData.SetCurrentPethealth(-1);
//             Death();
//         }
//     }

//     public void StartSinking()
//     {
//         CurrentStateData.RemoveCurrentPet();
//         // Disable Navmesh Component
//         GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

//         // Set rigidbody ke kinematic
//         GetComponent<Rigidbody>().isKinematic = true;

//         // Set isSinking
//         _isSinking = true;

//         if (manager != null)
//         {
//             manager.TryToSpawnNextPet(transform);

//         }
//         // Destroy Object
//         Destroy(gameObject, 2f);
//     }

//     public void Immortal()
//     {
//         _immortal = true;
//     }
// }
