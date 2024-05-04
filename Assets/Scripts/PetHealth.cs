using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetHealth : MonoBehaviour
{
    public int startHealth = 100;
    public int currHealth;
    public int startHealthReduced = 0;
    protected PetManager manager;
    public void SetManager(PetManager manager)
    {
        this.manager = manager;
    }

}