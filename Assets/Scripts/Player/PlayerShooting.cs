﻿using UnityEngine;
using UnityEngine.Events;
using System.Text;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections.Generic;

namespace Nightmare
{
    public class PlayerShooting : PausibleObject
    {
        //public int damagePerShot = 20;
        //public float timeBetweenBullets = 0.15f;
        //public float range = 100f;
        //public GameObject grenade;
        //public float grenadeSpeed = 200f;
        //public float grenadeFireDelay = 0.5f;
        public List<Weapon> weapons;

        int currentWeaponIndex = 0;

        //float timer;
        //Ray shootRay;
        //RaycastHit shootHit;
        //int shootableMask;
        ParticleSystem gunParticles;
        LineRenderer gunLine;
        Light gunLight;
        //float effectsDisplayTime = 0.2f;
        //public Light faceLight;
        //int grenadeStock = 99;

        //private UnityAction listener;

        void Awake()
        {

            InitializeWeapons();
            // Create a layer mask for the Shootable layer.
            //shootableMask = LayerMask.GetMask("Shootable"/*, "Enemy"*/);

            // Set up the references.
            gunParticles = GetComponent<ParticleSystem>();
            gunLine = GetComponent<LineRenderer>();
            gunLight = GetComponent<Light>();
            //faceLight = GetComponentInChildren<Light> ();

            //AdjustGrenadeStock(0);

            //listener = new UnityAction(CollectGrenade);

            //EventManager.StartListening("GrenadePickup", CollectGrenade);

            //StartPausible();
        }

        void InitializeWeapons()
        {
            //print(weapons.Count);
            foreach (var weapon in weapons) { weapon.enabled = false; }
            weapons[currentWeaponIndex].enabled = true;
        }

        void SwitchWeapon(int newWeaponIndex)
        {
            // Deactivate the current weapon
            weapons[currentWeaponIndex].gameObject.SetActive(false);

            // Update the index to the new weapon
            currentWeaponIndex = newWeaponIndex;

            // Activate the new weapon
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }

        //void OnDestroy()
        //{
        //    EventManager.StopListening("GrenadePickup", CollectGrenade);
        //    StopPausible();
        //}

        void Update()
        {
            //            if (isPaused)
            //                return;

            //            // Add the time since Update was last called to the timer.
            //            timer += Time.deltaTime;

            //#if !MOBILE_INPUT
            //            if (timer >= timeBetweenBullets && Time.timeScale != 0)
            //            {
            //                // If the Fire1 button is being press and it's time to fire...
            //                if (Input.GetButton("Fire2") && grenadeStock > 0)
            //                {
            //                    // ... shoot a grenade.
            //                    ShootGrenade();
            //                }

            //                // If the Fire1 button is being press and it's time to fire...
            //                else if (Input.GetButton("Fire1"))
            //                {
            //                    // ... shoot the gun.
            //                    Shoot();
            //                }
            //            }

            //#else
            //            // If there is input on the shoot direction stick and it's time to fire...
            //            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            //            {
            //                // ... shoot the gun
            //                Shoot();
            //            }
            //#endif
            //            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            //            if(timer >= timeBetweenBullets * effectsDisplayTime)
            //            {
            //                // ... disable the effects.
            //                DisableEffects ();
            //            }

            //timer += Time.deltaTime;

            //Weapon currWeapon = weapons[currentWeaponIndex];
            //bool validAttack = currWeapon.ValidAttack(timer);
            //bool invalidEffects = currWeapon.InvalidEffects(timer);

            //if (Input.GetButton("Fire1") && validAttack)
            //{
            //    Shoot();
            //}

            //if (invalidEffects)
            //{
            //    DisableEffects();
            //}

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchWeapon(0);
                gunLight.intensity = 1;
                gunLine.startWidth = 0.05f;
                gunLine.endWidth = 0.05f;
                gunParticles.startSize = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchWeapon(1);
                gunLight.intensity = 5;
                gunLine.startWidth = 0.09f;
                gunLine.endWidth = 0.05f;
                gunParticles.startSize = 3;

            }

            weapons[currentWeaponIndex].UpdateAttack();
        }

        //public void DisableEffects()
        //{
        //    // Disable the line renderer and the light.
        //    gunLine.enabled = false;
        //    ////faceLight.enabled = false;
        //    gunLight.enabled = false;
        //}


        //void Shoot()
        //{
        //    // Reset the timer.
        //    timer = 0f;

        //    // Play the gun shot audioclip.
        //    gunAudio.Play();

        //    // Enable the lights.
        //    gunLight.enabled = true;
        //    //faceLight.enabled = true;

        //    // Stop the particles from playing if they were, then start the particles.
        //    gunParticles.Stop();
        //    gunParticles.Play();

        //    // Enable the line renderer and set it's first position to be the end of the gun.
        //    gunLine.enabled = true;
        //    gunLine.SetPosition(0, transform.position);

        //    // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        //    shootRay.origin = transform.position;
        //    shootRay.direction = transform.forward;

        //    // Perform the raycast against gameobjects on the shootable layer and if it hits something...

        //    Weapon currWeapon = weapons[currentWeaponIndex];

        //    if (Physics.Raycast(shootRay, out shootHit, currWeapon.range, shootableMask))
        //    {
        //        // Try and find an EnemyHealth script on the gameobject hit.
        //        EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

        //        // If the EnemyHealth component exist...
        //        if (enemyHealth != null)
        //        {
        //            // ... the enemy should take damage.
        //            enemyHealth.TakeDamage(currWeapon.baseDamage, shootHit.point);
        //        }

        //        // Set the second position of the line renderer to the point the raycast hit.
        //        gunLine.SetPosition(1, shootHit.point);
        //    }
        //    // If the raycast didn't hit anything on the shootable layer...
        //    else
        //    {
        //        // ... set the second position of the line renderer to the fullest extent of the gun's range.
        //        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * currWeapon.range);
        //    }
        //}

        //private void ChangeGunLine(float midPoint)
        //{
        //    AnimationCurve curve = new AnimationCurve();

        //    curve.AddKey(0f, 0f);
        //    curve.AddKey(midPoint, 0.5f);
        //    curve.AddKey(1f, 1f);

        //    gunLine.widthCurve = curve;
        //}

        //public void CollectGrenade()
        //{
        //    AdjustGrenadeStock(1);
        //}

        //private void AdjustGrenadeStock(int change)
        //{
        //    grenadeStock += change;
        //    GrenadeManager.grenades = grenadeStock;
        //}

        //void ShootGrenade()
        //{
        //    AdjustGrenadeStock(-1);
        //    timer = timeBetweenBullets - grenadeFireDelay;
        //    GameObject clone = PoolManager.Pull("Grenade", transform.position, Quaternion.identity);
        //    EventManager.TriggerEvent("ShootGrenade", grenadeSpeed * transform.forward);
        //    //GameObject clone = Instantiate(grenade, transform.position, Quaternion.identity);
        //    //Grenade grenadeClone = clone.GetComponent<Grenade>();
        //    //grenadeClone.Shoot(grenadeSpeed * transform.forward);
        //}
    }
}