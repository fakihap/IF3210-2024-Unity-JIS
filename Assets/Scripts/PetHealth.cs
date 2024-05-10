using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetHealth : MonoBehaviour
{
    public int startHealth = 100;
    public int currHealth = 100;
    protected bool isImmortal;
    public int startHealthReduced = 0;
    public bool isEnemy;
    protected PetManager manager;
    public void SetManager(PetManager manager)
    {
        this.manager = manager;
    }
    public void setImmortal() {
        if (isEnemy) return; 
        isImmortal = !isImmortal;
        if (isImmortal) {
            print("cheat is immortal is on");
        } else {
            print("cheat is immortal is off");
        }
    }

}