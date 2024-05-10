using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject keroco;
    public GameObject kepalaKeroco;
    public GameObject jenderal;
    private int levelType;
    public List<Vector3> listPosition;
    private float spawnKeroco;
    private int spawnKepalaKeroco;
    private int spawnJenderal;
    private float spawnKerocoTimer;
    private float spawnKepalaKerocoTimer;
    private float spawnJenderalTimer;
    void Start()
    {
        print("ini level berapa adik adick "+ CurrStateData.GetDifficultyLevel()); 
        if(CurrStateData.GetDifficultyLevel() == null){
            CurrStateData.SetDifficultyLevel(0);
            ChangeLevel(0);
        } 
        else{
            ChangeLevel(CurrStateData.GetDifficultyLevelIndex());
        }
    }

    public void ChangeLevel(int type){
        levelType = type;
        print("ini ganti level berapa adik adick "+ CurrStateData.GetDifficultyLevel());  
        spawnKepalaKerocoTimer = 0f;
        spawnKerocoTimer = 0f;
        spawnJenderalTimer = 0f;
        if(type==1 || CurrStateData.GetDifficultyLevel()==null){

            spawnKeroco = 10;
            spawnKepalaKeroco = 20;
            spawnJenderal = 30;
        }
        else if(type==2){
            spawnKeroco = 5;
            spawnKepalaKeroco = 10;
            spawnJenderal = 15;
        }
        else{
            spawnKeroco = 3;
            spawnKepalaKeroco = 6;
            spawnJenderal = 9;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnKerocoTimer += Time.deltaTime;
        if (spawnKerocoTimer >= spawnKeroco)
        {
            // print("spawn kerocoooook");
            Spawn(1);
            spawnKerocoTimer = 0f;
        }

        spawnKepalaKerocoTimer += Time.deltaTime;
        if (spawnKepalaKerocoTimer >= spawnKepalaKeroco)
        {
            // print("spawn kerocoooook");
            Spawn(2);
            spawnKepalaKerocoTimer = 0f;
        }

        print("spawn jenderal timer " + spawnJenderalTimer + " spawn jenderal: " + spawnJenderal);
        spawnJenderalTimer += Time.deltaTime;
        if (spawnJenderalTimer >= spawnJenderal)
        {
            Spawn(3);
            spawnJenderalTimer = 0f;
        }

    }

    void Spawn(int type){
        Vector3 enemyPosition = listPosition[UnityEngine.Random.Range(0, listPosition.Count)];

        Quaternion rotation = Quaternion.Euler(0, 0, 0);            
        
        if(type == 1)
            Instantiate (keroco, enemyPosition, rotation);
        else if(type == 2)
            Instantiate (kepalaKeroco, enemyPosition, rotation);
        else if(type == 3)
            Instantiate (jenderal, enemyPosition, rotation);
    }
}
