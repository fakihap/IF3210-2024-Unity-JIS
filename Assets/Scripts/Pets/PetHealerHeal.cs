using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetHealerHeal : MonoBehaviour
{
    private GameObject player;
    public float healDelay = 2f;
    public int healAmount = 10;
    float time;
    bool playerInRange;
    PlayerHealth playerHealth;
    AudioSource healAudio;
    
    // Start is called before the first frame update
    void Awake()
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
        // Debug.Log("Heal circle magnitue: " + (transform.position - player.transform.position).magnitude);
        if ((transform.position - player.transform.position).magnitude <= 8.5)
        {
            // Debug.Log("Player in range masuk");
            playerInRange = true;
        }
        else
        {
            // Debug.Log("Player in range masuk");
            playerInRange = false;
        }

        time += Time.deltaTime;
        if(playerInRange && time >= healDelay && !PauseManager.IsPaused())
        {
            Debug.Log("Panggil Heal");
            Heal();
        }
    }

    void Heal()
    {
        Debug.Log("Dalam Heal");
        healAudio.Play();
        time = 0f;
        playerHealth.AddHealth(healAmount);
    }
}