using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class PetManager : MonoBehaviour
{
    public GameObject petAttacker;
    public GameObject petHealer;
    public GameObject petBuffer;
    public static Transform nextTransform;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
}