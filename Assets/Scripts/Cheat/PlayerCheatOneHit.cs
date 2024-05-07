using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class PlayerCheatOneHit : MonoBehaviour
{

    public string[] cheatCodeOneHit = new string[] { "o", "n", "e", "h", "i", "t" };
    private int indexOneHit;
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
        indexOneHit = 0;

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
            if (Input.GetKeyDown(cheatCodeOneHit[indexOneHit]))
            {
                indexOneHit++;

            }
            else
            {
                indexOneHit = 0;
            }
        }

        if (indexOneHit == cheatCodeOneHit.Length)
        {
            isOn = !isOn;
            indexOneHit = 0;
            if (isOn)
            {
                print("OneHit on");
                rifle.baseDamage = 999999;
                shotgun.baseDamage = 999999;
                sword.baseDamage = 999999;
            }
            else
            {
                print("OneHit off");
                rifle.baseDamage = rifleBaseDamage;
                shotgun.baseDamage = shotgunBaseDamage;
                sword.baseDamage = swordBaseDamage;
            }
        }
    }
}
