using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class PlayerCheatImmortal : MonoBehaviour
{
    public string[] cheatCodePlayerImmortal = new string[] { "n", "o", "d", "m", "g" };
    private int indexCheat;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        indexCheat = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCodePlayerImmortal[indexCheat]))
            {
                indexCheat++;
            }
            else
            {
                indexCheat = 0;
            }
        }

        if (indexCheat == cheatCodePlayerImmortal.Length)
        {
            playerHealth.setNoDmg();
            if (playerHealth.godMode) {
                print("player immortal is on");
            } else {
                print("player immortal is off");
            }
            indexCheat = 0;

        }
    }
}
