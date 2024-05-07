using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetJenderalHeal : MonoBehaviour
{
    public GameObject jenderal;
    public float healDelay = 2f;
    public int healAmount = 10;
    float time;
    bool playerInRange;
    EnemyHealth jenderalHealth;
    AudioSource healAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        jenderal = transform.parent.gameObject;
        jenderalHealth = jenderal.GetComponent<EnemyHealth>();
        healAudio = GetComponent<AudioSource>();
        time = 10f;

        if((transform.position - jenderal.transform.position).magnitude <= 8.5)
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
        if ((transform.position - jenderal.transform.position).magnitude <= 8.5)
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
        jenderalHealth.AddHealth(healAmount);
    }
}