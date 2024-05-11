using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetManager : MonoBehaviour
{
    public GameObject petAttacker;
    public GameObject petHealer;
    // public GameObject petBuffer;
    public static Transform nextTransform;
    public static bool isSpawnNewPet = false;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnCurrPet();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawnNewPet)
        {
            Debug.Log("Spawn New Pet");
            SpawnCurrPet();
            isSpawnNewPet = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Ganti Pet");
            SwitchPet();
        } 
    }

    public void FixedUpdate()
    {

    }

    public void SpawnCurrPet()
    {
        print("pets:" +  CurrStateData.currGameData.pets[0] + " "+ CurrStateData.currGameData.pets[1] );
        print("pets health:" +CurrStateData.currGameData.petHealth[0] + " "+ CurrStateData.currGameData.petHealth[1]);
        int petId = CurrStateData.GetCurrentPet();
        Debug.Log("Curr PetID: " + petId);
        if(petId != -1)
        {
            if(CurrStateData.GetCurrentPetHealth() <= 0 && !isSpawnNewPet)
            {
                CurrStateData.RemoveCurrentPet();
                petId = CurrStateData.GetCurrentPet();
                if(petId != -1)
                {
                    GameObject pet = null;
                    if(petId == 0)
                    {
                        pet = Instantiate(petAttacker, player.transform.position + Vector3.right, player.transform.rotation);
                        Debug.Log("Pet initiated");
                        pet.SetActive(true);
                        Debug.Log("Pet is set to be active");
                    }
                    else
                    {
                        pet = Instantiate(petHealer, player.transform.position + Vector3.right, player.transform.rotation);
                        Debug.Log("Pet initiated");
                        pet.SetActive(true);
                        Debug.Log("Pet is set to be active");
                    }

                    var petHealth = pet.GetComponent<PetHealth>();
                    if (petHealth != null)
                    {
                        petHealth.currHealth = CurrStateData.currGameData.currPetHealth;
                        petHealth.SetManager(this);
                    }
                    else
                    {
                        Debug.Log("petHealth is null");
                    }
                }
            }
            else
            {
                GameObject pet = null;
                if(petId == 0)
                {
                    pet = Instantiate(petAttacker, player.transform.position + (Vector3.right * 0.5f), player.transform.rotation);
                }
                else
                {
                    pet = Instantiate(petHealer, player.transform.position + (Vector3.right * 0.5f), player.transform.rotation);
                }

                
                var petHealth = pet.GetComponent<PetHealth>();
                if (petHealth != null)
                {
                    petHealth.currHealth = CurrStateData.currGameData.currPetHealth;                       
                    petHealth.SetManager(this);
                }
                else
                {
                    Debug.Log("petHealth is null");
                }
            }
        }
    }

    public void SpawnNextPet(Transform transform)
    {
        int petId = CurrStateData.GetCurrentPet();
        if(petId != -1)
        {
            GameObject pet = null;
            if (petId == 0)
            {
                pet = Instantiate(petAttacker, transform.position, transform.rotation);
            }
            else if (petId == 1)
            {
                pet = Instantiate(petHealer, transform.position, transform.rotation);
            }

            var petHealth = pet.GetComponent<PetHealth>();
            if (petHealth != null)
            {
                petHealth.currHealth = CurrStateData.currGameData.petHealth[0];                
                petHealth.SetManager(this);
            }
        }
    }

    private void SwitchPet()
    { 
        DestroyCurrentPet();
        CurrStateData.SwitchPets();
        SpawnCurrPet();
    }

    private void DestroyCurrentPet()
    {
        GameObject currentPet;
        if(CurrStateData.GetCurrentPet() == 0)
        {
            Debug.Log("Remove Pet Attacker");
            currentPet = GameObject.FindGameObjectWithTag("PetAttacker");
        }
        else
        {
            Debug.Log("Remove Pet Healer");
            currentPet = GameObject.FindGameObjectWithTag("PetHealer");
        }
        if (currentPet != null)
        {
            Destroy(currentPet);
        }
    }
}