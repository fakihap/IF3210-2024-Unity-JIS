using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class PlayerCheatMotherlode : MonoBehaviour
{

    public string[] cheatCodeMotherlode = new string[] { "m", "o", "t", "h", "e", "r" };
    private int indexMotherlode;
    public Rifle rifle;
    public Shotgun shotgun;
    public Sword sword;
    private int rifleBaseDamage;
    private int shotgunBaseDamage;
    private int swordBaseDamage;
    private bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        indexMotherlode = 0;

        rifleBaseDamage = rifle.baseDamage;
        shotgunBaseDamage = shotgun.baseDamage;
        swordBaseDamage = sword.baseDamage;

        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCodeMotherlode[indexMotherlode]))
            {
                indexMotherlode++;

            }
            else
            {
                indexMotherlode = 0;
            }
        }

        if (indexMotherlode == cheatCodeMotherlode.Length)
        {

            if (!isOn)
            {
                print("motherlode on");
                rifle.baseDamage = 999999;
                shotgun.baseDamage = 999999;
                sword.baseDamage = 999999;
            }
            else
            {
                print("motherlode off");
                rifle.baseDamage = rifleBaseDamage;
                shotgun.baseDamage = shotgunBaseDamage;
                sword.baseDamage = swordBaseDamage;
            }
            indexMotherlode = 0;
            isOn = !isOn;
        }
    }
}
