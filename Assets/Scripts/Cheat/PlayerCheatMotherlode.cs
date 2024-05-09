using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class PlayerCheatMotherlode : MonoBehaviour
{

    public string[] cheatMotherlode = new string[] { "m", "o", "t", "h", "e", "r" };
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatMotherlode[index]))
            {
                index++;

            }
            else
            {
                index = 0;
            }
        }

        if (index == cheatMotherlode.Length)
        {
            index = 0;
            if (!CurrStateData.currGameData.motherlode)
            {
                print("Motherlode on");
                CurrStateData.currGameData.motherlode = true;
            }
            else
            {
                print("Motherlode off");
                CurrStateData.currGameData.motherlode = false;
            }

        }
    }
}
