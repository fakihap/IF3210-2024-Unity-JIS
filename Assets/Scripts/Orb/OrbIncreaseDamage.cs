using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class OrbIncreaseDamage : MonoBehaviour
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
            IncreaseDamage();
            Despawn();
            print("Increase damage via orb");
        }
    }
    bool OrbInrange()
    {
        return (player.transform.position - transform.position).magnitude <= 1.5;
    }

    private void IncreaseDamage()
    {
        // i want to increase damage of player 10% with 10 second
        Weapon rifle = GameObject.FindGameObjectWithTag("Rifle").GetComponent<Rifle>();
        Weapon sword = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Sword>();
        Weapon shotgun = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Shotgun>();

        rifle.IncreaseDamage(10);
        sword.IncreaseDamage(10);
        shotgun.IncreaseDamage(10);
        
    }



    private void Despawn()
    {
        Destroy(gameObject);
    }
}
