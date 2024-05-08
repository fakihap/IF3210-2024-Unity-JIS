using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Nightmare
{
    public class Rifle : Weapon
    {
        public LineRenderer gunLine;
        public Light gunLight;
        public ParticleSystem gunParticles;
        public GameObject gunBarrelEnd;

        float timer;
        Ray shootRay;
        RaycastHit shootHit;
        int shootableMask;
        float effectsDisplayTime = 0.2f;


        public override void Attack()
        {
            timer = 0f;

            attackSound.Play();

            gunLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop();
            gunParticles.Play();

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;
            gunLine.SetPosition(0, gunBarrelEnd.transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = gunBarrelEnd.transform.position;
            shootRay.direction = gunBarrelEnd.transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                print("Enemy is hit123");
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                print(enemyHealth);
                // If the EnemyHealth component exist...
                print("this is enemny health "+enemyHealth);
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    print("Enemy is take damage "+baseDamage);

                    // add orb
                    PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                    int damage = baseDamage+10*playerMovement.OrbIncreaseDamageCount;
                    if(playerMovement.DamageDecreaseByRaja){
                        print("Damage decrease by raja");
                        damage = damage * 80 / 100;
                    }
                    enemyHealth.TakeDamage(damage, shootHit.point);
                }
                else
                {
                    print("Enemy is not take damage");
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition(1, shootHit.point);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }

        public override void DisableEffects()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
            //faceLight.enabled = false;
            gunLight.enabled = false;
        }

        private void Awake()
        {
            shootableMask = LayerMask.GetMask("Environment");
        }

        public override void UpdateAttack()
        {
            timer += Time.deltaTime;
            //print("Mouse Pressed");

            if (Input.GetButton("Fire1") && timer >= attackSpeed && !PauseManager.IsPaused())
            {
                //print("Mouse Pressed");
                Attack();
            }

            if (timer >= attackSpeed * effectsDisplayTime)
            {
                DisableEffects();
            }
        }
        
        public override void IncreaseDamage(int damageIncrease)
        {
            baseDamage += baseDamage * damageIncrease / 100;
        }

    }

}
