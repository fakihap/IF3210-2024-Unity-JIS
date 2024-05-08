using System.Collections;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace Nightmare
{
    public class PlayerMovement : PausibleObject
    {
        public float speed = 6f;
        private float originalSpeed; // untuk menyimpan nilai kecepatan asli sebelum peningkatan
        public int OrbIncreaseDamageCount = 0 ;
        public bool DamageDecreaseByRaja;

        Vector3 movement;
        Animator anim;
        Rigidbody playerRigidBody;
        int floorMask;
        readonly float camRayLength = 1000f;

        private void Awake()
        {
            floorMask = LayerMask.GetMask("Floor");
            anim = GetComponent<Animator>();
            playerRigidBody = GetComponent<Rigidbody>();
            originalSpeed = speed; // menyimpan nilai awal kecepatan
            DamageDecreaseByRaja = false;
        }

        private void FixedUpdate()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Move(h, v);
            Turning();
            Animating(h, v);
        }

        void Move(float h, float v)
        {
            movement = new Vector3(h, 0, v);;

            movement = movement.normalized * speed * Time.deltaTime;
            
            CurrStateData.distanceTravelled += movement.magnitude;
            // print("distance travelled: " + CurrStateData.distanceTravelled);
            playerRigidBody.MovePosition(transform.position + movement);
        }

        void Turning()
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out RaycastHit floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                playerRigidBody.MoveRotation(newRotation);
            }
        }

        void Animating(float h, float v)
        {
            bool walking = h != 0f || v != 0f;
            anim.SetBool("IsWalking", walking);
        }

        public void OrbIncreaseSpeed(float duration, float multiplier)
        {
            StartCoroutine(IncreaseSpeedForDuration(duration, multiplier));
        }

        IEnumerator IncreaseSpeedForDuration(float duration, float multiplier)
        {
            speed *= multiplier; // meningkatkan kecepatan sesuai multiplier
            yield return new WaitForSeconds(duration);
            speed = originalSpeed; // mengembalikan kecepatan ke nilai semula setelah durasi selesai
        }
        public void AddOrbIncreseDamage(){
            OrbIncreaseDamageCount++;
        }

        public void MultiplierSpeed(float multiplier){
            speed *= multiplier;
        }
        public void ChangeDamageDecreaseByRaja(){
            print("change damage decrease by raja");
            if(DamageDecreaseByRaja){
                DamageDecreaseByRaja = false;
            }
            else{
                DamageDecreaseByRaja = true;
            }
        }
    }
}
