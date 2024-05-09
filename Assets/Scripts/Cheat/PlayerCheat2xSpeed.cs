using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class PlayerCheat2xSpeed : MonoBehaviour
{
    public string[] cheatCodePlayerSpeed = new string[] { "s", "p", "e", "e", "d" };
    private int indexCheat;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        indexCheat = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCodePlayerSpeed[indexCheat]))
            {
                indexCheat++;
            }
            else
            {
                indexCheat = 0;
            }
        }

        if (indexCheat == cheatCodePlayerSpeed.Length)
        {
            playerMovement.setFlashMode();
            if (playerMovement.isFlash) {
                print("player 2x speed is on");
            } else {
                print("player 2x speed is off");
            }
            indexCheat = 0;
        }
    }
}
