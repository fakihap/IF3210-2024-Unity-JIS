using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject kerocoPrefab;
    public GameObject kepalaKerocoPrefab;
    public GameObject jenderalPrefab;
    
    private int levelType;
    private int levelBefore;
    
    public List<Vector3> listPosition;
    
    private float spawnKerocoInterval;
    private float spawnKepalaKerocoInterval;
    private float spawnJenderalInterval;
    
    private float spawnKerocoTimer;
    private float spawnKepalaKerocoTimer;
    private float spawnJenderalTimer;
    
    void Start()
    {
        levelType = 0;
        levelBefore = -1;
        ChangeLevel(PlayerPrefs.GetInt("difficultyLevelIndex"));
    }

    public void ChangeLevel(int type)
    {
        if (levelBefore == type)
        {
            return;
        }
        
        levelType = type;
        levelBefore = type;
        
        spawnKepalaKerocoTimer = 0f;
        spawnKerocoTimer = 0f;
        spawnJenderalTimer = 0f;
        
        switch (type)
        {
            case 0:
                spawnKerocoInterval = 10f;
                spawnKepalaKerocoInterval = 20f;
                spawnJenderalInterval = 30f;
                break;
            case 1:
                spawnKerocoInterval = 5f;
                spawnKepalaKerocoInterval = 10f;
                spawnJenderalInterval = 15f;
                break;
            default:
                spawnKerocoInterval = 3f;
                spawnKepalaKerocoInterval = 6f;
                spawnJenderalInterval = 9f;
                break;
        }
        print("spawwnn "+spawnKerocoInterval+" "+spawnKepalaKerocoInterval+" "+spawnJenderalInterval);
    }

    void Update()
    {
        spawnKerocoTimer += Time.deltaTime;
        if (spawnKerocoTimer >= spawnKerocoInterval)
        {
            SpawnEnemy(kerocoPrefab);
            spawnKerocoTimer = 0f;
        }

        spawnKepalaKerocoTimer += Time.deltaTime;
        if (spawnKepalaKerocoTimer >= spawnKepalaKerocoInterval)
        {
            SpawnEnemy(kepalaKerocoPrefab);
            spawnKepalaKerocoTimer = 0f;
        }

        spawnJenderalTimer += Time.deltaTime;
        if (spawnJenderalTimer >= spawnJenderalInterval)
        {
            SpawnEnemy(jenderalPrefab);
            spawnJenderalTimer = 0f;
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 enemyPosition = listPosition[Random.Range(0, listPosition.Count)];
        Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
    }
}
