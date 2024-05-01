using UnityEngine;
using UnityEngine.Events;
using System.Text;
using UnitySampleAssets.CrossPlatformInput;

namespace Nightmare
{
    public class KepalaKerocoAttack : PausibleObject
    {
        public int damagePerShot = 10;
        public float timeBetweenBullets = 0.15f;
        public float range = 40f;
        public GameObject grenade;
        public float grenadeSpeed = 200f;
        public float grenadeFireDelay = 0.5f;

        float timer;

        float shotTimer;
        Ray shootRay = new Ray();
        RaycastHit shootHit;
        int shootableMask;
        ParticleSystem gunParticles;
        LineRenderer gunLine;
        AudioSource gunAudio;
        Light gunLight;
		public Light faceLight;
        float effectsDisplayTime = 0.2f;

        void Awake ()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask ("Shootable", "Enemy");

            // Set up the references.
            gunParticles = GetComponent<ParticleSystem> ();
            gunLine = GetComponent <LineRenderer> ();
            gunAudio = GetComponent<AudioSource> ();
            gunLight = GetComponent<Light> ();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void Update ()
        {
            if (isPaused)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Shoot();
                timer = 5f;
            }

        }


        public void DisableEffects ()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
			// faceLight.enabled = false;
            gunLight.enabled = false;
        }


        void Shoot ()
        {
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            gunAudio.Play ();

            // Enable the lights.
            gunLight.enabled = true;
			// faceLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop ();
            gunParticles.Play ();

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;
            gunLine.SetPosition (0, transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                PlayerHealth playerHealth = shootHit.collider.GetComponent <PlayerHealth> ();

                // If the EnemyHealth component exist...
                // ... the enemy should take damage.
                playerHealth?.TakeDamage(damagePerShot);

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition (1, shootHit.point);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            }
        }
        
    }
}