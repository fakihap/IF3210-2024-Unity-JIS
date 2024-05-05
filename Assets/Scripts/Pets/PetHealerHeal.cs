using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetHealerHeal : MonoBehaviour
{
    public GameObject player;
    public float healDelay = 10f;
    public int healAmount = 10;
    float time;
    bool playerInRange;
    PlayerHealth playerHealth;
    AudioSource healAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        healAudio = GetComponent<AudioSource>();
        time = 10f;

        if((transform.position - player.transform.position).magnitude <= 8.5)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

        // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).magnitude <= 8.5)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        time = Time.deltaTime;
        if(playerInRange && time >= healDelay)
        {
            Heal();
        }
    }

    void Heal()
    {
        healAudio.Play();
        time = 0f;
        playerHealth.AddHealth(healAmount);
    }
}