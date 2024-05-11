﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput;

namespace Nightmare
{
    public class PlayerMovement : PausibleObject
    {
        public float speed = 6f;
        private float originalSpeed; // untuk menyimpan nilai kecepatan asli sebelum peningkatan
        public int OrbIncreaseDamageCount = 0;
        public bool DamageDecreaseByRaja;
        public Text saveText;

        Vector3 movement;
        Animator anim;
        Rigidbody playerRigidBody;
        GameObject safeHouse;
        int floorMask;
        bool canSave;
        readonly float camRayLength = 1000f;
        public bool isFlash = false;

        private void Awake()
        {
            floorMask = LayerMask.GetMask("Floor");
            anim = GetComponent<Animator>();
            playerRigidBody = GetComponent<Rigidbody>();
            originalSpeed = speed; // menyimpan nilai awal kecepatan
            DamageDecreaseByRaja = false;
            safeHouse = GameObject.FindGameObjectWithTag("SafeHouse");
            isFlash = false;
        }

        private void FixedUpdate()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");


            if (canSave)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        print("Save key is pressed");
                        SaveGame();
                    }
                }
            }

            // kill all mobs when we are inside safezone
            if (safeHouse != null) {
                float distanceToSafeHouse = Vector3.Distance(transform.position, safeHouse.transform.position);
                if (distanceToSafeHouse < 5)
                {
                    // Kill all mobs
                    GameObject[] mobs = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject mob in mobs)
                    {
                        HealthBar healthBar = mob.GetComponent<HealthBar>();
                        if (healthBar != null)
                        {
                            healthBar.DisableHealthBar();
                        }
                        Destroy(mob);
                    }
                    GameDirector.Instance.PauseDirector();
                }
                else
                {
                    GameDirector.Instance.UnpauseDirector();
                }
            }
            

            Move(h, v);
            Turning();
            Animating(h, v);

        }

        private void OnTriggerEnter(Collider other)
        {
            print("Player collides with: " + other.name);
            if (other.gameObject == safeHouse)
            {
                //print("I hit the safe house (player movement)");
                //saveText.enabled = true;
                canSave = true;
            }
        }

        private void SaveGame()
        {
            //CurrStateData currData = CurrStateData.GetInstance();
            //string output;

            string currFileName = "Slot";

            currFileName += CurrStateData.currGameData.currentSlot.ToString() + ".dat";

            CurrStateData.currGameData.playerCoordinates = transform.position;

            print("Current slot: " + CurrStateData.currGameData.currentSlot.ToString());
            print("Current file name: " + currFileName);


            if (FileManager.WriteToFile(currFileName, CurrStateData.ToJson()))
            {
                print("Save successful to " + currFileName);
                // CurrStateData.currGameData.distanceTravelled = 0;
            }
            

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == safeHouse)
            {
                //print("I hit the safe house (player movement)");
                //saveText.enabled = false;
                canSave = false;
            }
        }

        void Move(float h, float v)
        {
            movement = new Vector3(h, 0, v);;

            movement = movement.normalized * speed * Time.deltaTime;
            
            CurrStateData.currGameData.distanceTravelled += movement.magnitude;
            
            float savedDistanceTravelled = PlayerPrefs.GetFloat("distanceTravelled");
            // print(savedDistanceTravelled);
            PlayerPrefs.SetFloat("distanceTravelled", savedDistanceTravelled + movement.magnitude);            

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
            speed = originalSpeed * multiplier; // meningkatkan kecepatan sesuai multiplier
            yield return new WaitForSeconds(duration);
            speed /= multiplier; // mengembalikan kecepatan ke nilai semula setelah durasi selesai
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

        public void setFlashMode()
        {
            if (!isFlash) {
                speed *= 2;
            } else {
                speed /= 2;
            }
            isFlash = !isFlash;
        }
    }
}
