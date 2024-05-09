using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject keroco;
    public GameObject kepalaKeroco;
    public GameObject jenderal;
    public int gameLevel;
    public List<Vector3> listPosition;
    private float spawnKeroco;
    private int spawnKepalaKeroco;
    private int spawnJenderal;
    private float spawnKerocoTimer;
    private float spawnKepalaKerocoTimer;
    private float spawnJenderalTimer;
    void Start()
    {

        // print("game level " + gameLevel);
        if(gameLevel>5){
            spawnKeroco = 5;
            spawnKepalaKeroco = 10;
            spawnJenderal = 15;
        }
        else{
            spawnKeroco = 10 - gameLevel;
            spawnKepalaKeroco = 20 - gameLevel*2;
            spawnJenderal = 30 - gameLevel*3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        print("game level " + gameLevel);
        print("spawn keroco " + spawnKeroco);
        print("spawn kepala keroco " + spawnKepalaKeroco);
        print("spawn jenderal " + spawnJenderal);
        // i want spawn keroco, kepalakeroco and jenderal based on game level
        // if game level is 1, spawn 6 keroco, 12 kepala keroco, 18 jenderal
        // if game level is 2, spawn 5 keroco, 10 kepala keroco, 15 jenderal
        // if game level is 3, spawn 4 keroco, 8 kepala keroco, 12 jenderal

        spawnKerocoTimer += Time.deltaTime;
        if (spawnKerocoTimer >= spawnKeroco)
        {
            print("spawn kerocoooook");
            Spawn(1);
            spawnKerocoTimer = 0f;
        }

        spawnKepalaKerocoTimer += Time.deltaTime;
        if (spawnKepalaKerocoTimer >= spawnKepalaKeroco)
        {
            print("spawn kerocoooook");
            Spawn(2);
            spawnKepalaKerocoTimer = 0f;
        }

        // print("spawn jenderal timer " + spawnJenderalTimer + " spawn jenderal: " + spawnJenderal);
        spawnJenderalTimer += Time.deltaTime;
        if (spawnJenderalTimer >= spawnJenderal)
        {
            print("spawn kerocoooook");
            Spawn(3);
            spawnJenderalTimer = 0f;
        }

    }

    void Spawn(int type){
        // i want spawn keroco kepala keroco and jenderal based on list position (x,0,z)
        // if type is 1, spawn keroco
        // if type is 2, spawn kepala keroco
        // if type is 3, spawn jenderal
        
        Vector3 enemyPosition = listPosition[Random.Range(0, listPosition.Count)];

        Quaternion rotation = Quaternion.Euler(0, 0, 0);            
        
        if(type == 1)
            Instantiate (keroco, enemyPosition, rotation);
        else if(type == 2)
            Instantiate (kepalaKeroco, enemyPosition, rotation);
        else if(type == 3)
            Instantiate (jenderal, enemyPosition, rotation);
    }
}
