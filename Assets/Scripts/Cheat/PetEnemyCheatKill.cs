using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetEnemyCheatKill : MonoBehaviour
{
    public string[] cheatCodePetKill = new string[] { "p", "e", "t", "k", "i", "l", "l" };
    private int indexCheat;
    public PetBuffHealth petEnemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        indexCheat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCodePetKill[indexCheat]))
            {
                indexCheat++;
            }
            else
            {
                indexCheat = 0;
            }
        }

        if (indexCheat == cheatCodePetKill.Length)
        {
            print("instant kill pet enemy");
            petEnemyHealth.TakeDamage(999999999);
        }
    }
}
