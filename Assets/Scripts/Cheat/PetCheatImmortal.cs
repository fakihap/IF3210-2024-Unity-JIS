using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCheatImmortal : MonoBehaviour
{
    public string[] cheatCodePetImmortal = new string[] { "p", "e", "t", "g", "o", "d" };
    private int indexCheat;
    public PetHealth petHealth;
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
            if (Input.GetKeyDown(cheatCodePetImmortal[indexCheat]))
            {
                indexCheat++;
            }
            else
            {
                indexCheat = 0;
            }
        }

        if (indexCheat == cheatCodePetImmortal.Length)
        {
            

            petHealth.setImmortal();
            indexCheat = 0;

        }
    }
}
