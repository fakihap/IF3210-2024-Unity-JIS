using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Nightmare
{
    public class Shotgun : Weapon
    {
        public LineRenderer gunLine0;
        public LineRenderer gunLine1;
        public LineRenderer gunLine2;
        public LineRenderer gunLine3;
        public LineRenderer gunLine4;
        public Light gunLight;
        public ParticleSystem gunParticles;
        public GameObject gunBarrelEnd;
        public float bulletAngle = 3f;

        float timer;
        Ray shootRay;
        RaycastHit shootHit;
        int shootableMask;
        float effectsDisplayTime = 0.01f;


        public override void Attack()
        {
            timer = 0f;

            attackSound.Play();

            gunLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop();
            gunParticles.Play();

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine0.enabled = true;
            gunLine1.enabled = true;
            gunLine2.enabled = true;
            gunLine3.enabled = true;
            gunLine4.enabled = true;

            gunLine0.SetPosition(0, gunBarrelEnd.transform.position);
            gunLine1.SetPosition(0, gunBarrelEnd.transform.position);
            gunLine2.SetPosition(0, gunBarrelEnd.transform.position);
            gunLine3.SetPosition(0, gunBarrelEnd.transform.position);
            gunLine4.SetPosition(0, gunBarrelEnd.transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = gunBarrelEnd.transform.position;
            shootRay.direction = gunBarrelEnd.transform.forward;

            CurrStateData.currGameData.shotCount += 1;
            // print("shot accuracy: " + CurrStateData.GetShotAccuracy());

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                // Calculate the distance to the hit target
                float distanceToTarget = Vector3.Distance(shootRay.origin, shootHit.point);

                // Define the maximum and minimum damage values
                PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                int damage = baseDamage + 10 * playerMovement.OrbIncreaseDamageCount;
                if (playerMovement.DamageDecreaseByRaja)
                {
                    damage = damage * 80 / 100;
                }

                int maxDamage = damage;
                int minDamage = damage / 2; // Adjust as necessary

                // Calculate the damage based on the distance (closer targets take more damage)
                float damageFactor = 1 - (distanceToTarget / range);
                float calculatedDamage = Mathf.Lerp(minDamage, maxDamage, damageFactor);

                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                // If the EnemyHealth component exist...
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    enemyHealth.TakeDamage((int)calculatedDamage, shootHit.point);
                    CurrStateData.currGameData.hitCount += 1;
                    CurrStateData.currGameData.damageDealt += (int)calculatedDamage;
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine0.SetPosition(1, shootHit.point);
                gunLine1.SetPosition(1, shootRay.origin + Quaternion.Euler(-bulletAngle, 0f, 0f) * shootRay.direction * range);
                gunLine2.SetPosition(1, shootRay.origin + Quaternion.Euler(bulletAngle, 0f, 0f) * shootRay.direction * range);
                gunLine3.SetPosition(1, shootRay.origin + Quaternion.Euler(0f, bulletAngle, 0f) * shootRay.direction * range);
                gunLine4.SetPosition(1, shootRay.origin + Quaternion.Euler(0f, -bulletAngle, 0f) * shootRay.direction * range);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine0.SetPosition(1, shootRay.origin + shootRay.direction * range);
                gunLine1.SetPosition(1, shootRay.origin + Quaternion.Euler(-bulletAngle, 0f, 0f) * shootRay.direction * range);
                gunLine2.SetPosition(1, shootRay.origin + Quaternion.Euler(bulletAngle, 0f, 0f) * shootRay.direction * range);
                gunLine3.SetPosition(1, shootRay.origin + Quaternion.Euler(0f, bulletAngle, 0f) * shootRay.direction * range);
                gunLine4.SetPosition(1, shootRay.origin + Quaternion.Euler(0f, -bulletAngle, 0f) * shootRay.direction * range);
            }
        }

        public override void DisableEffects()
        {
            // Disable the line renderer and the light.
            gunLine0.enabled = false;
            gunLine1.enabled = false;
            gunLine2.enabled = false;
            gunLine3.enabled = false;
            gunLine4.enabled = false;
            //faceLight.enabled = false;
            gunLight.enabled = false;
        }

        private void Awake()
        {
            shootableMask = LayerMask.GetMask("Environment");

            //gunLine0 = GetComponent<LineRenderer>();
            //attackSound = GetComponent<AudioSource>();
            //gunLight = GetComponent<Light>();
            //gunParticles = GetComponent<ParticleSystem>();
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
