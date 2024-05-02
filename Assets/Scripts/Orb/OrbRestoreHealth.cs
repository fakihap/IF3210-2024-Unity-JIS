using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class OrbRestoreHealth : MonoBehaviour
{
    public float despawnTime = 5f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Despawn", despawnTime);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        if (OrbInrange())
        {
            HealPlayer();
            Despawn();
            print("heal via orb");
        }
    }
    bool OrbInrange()
    {
        return (player.transform.position - transform.position).magnitude <= 1.5;
    }

    private void HealPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        int currentHealth = playerHealth.currentHealth;
        playerHealth.AddHealth(currentHealth/5);
    }



    private void Despawn()
    {
        Destroy(gameObject);
    }
}
