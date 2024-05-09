using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetBuffEffect : MonoBehaviour
{
    GameObject enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
        PetBuffHealth petBuffHealth = enemy.GetComponent<PetBuffHealth>();
        if(enemy.GetComponent<JenderalAttack>() != null  && petBuffHealth != null)
        {
            enemy.GetComponent<JenderalAttack>().DoubleDamage();
        }
        else if(enemy.GetComponent<RajaAttack>() != null && petBuffHealth != null)
        {
            enemy.GetComponent<RajaAttack>().DoubleDamage();
        }
    }

        // Update is called once per frame
    void Update()
    {

    }

}