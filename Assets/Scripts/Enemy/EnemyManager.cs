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

    [SerializeField] private EnemyType enemyType;
    
    void Start()
    {
        levelType = PlayerPrefs.GetInt("difficultyLevelIndex");
        levelBefore = -1;
        ChangeLevel(PlayerPrefs.GetInt("difficultyLevelIndex"));
    }

    public void ChangeLevel(int type)
    {

        print("type "+type+" levelBefore "+levelBefore);
        if (levelBefore == type)
        {
            return;
        }

        print("masuk change level");
        
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
        // will only spawn desired enemy type
        ChangeLevel(PlayerPrefs.GetInt("difficultyLevelIndex"));

        SpawnKeroco();
        if(kepalaKerocoPrefab != null){
            SpawnKepalaKeroco();
        }
        if(jenderalPrefab != null){
            SpawnJenderal();
        }
    }

    void SpawnKeroco() {
        // if (enemyType != EnemyType.Keroco && enemyType != EnemyType.Any) {
        //     return;
        // }

        spawnKerocoTimer += Time.deltaTime;
        if (spawnKerocoTimer >= spawnKerocoInterval)
        {
            SpawnEnemy(kerocoPrefab);
            spawnKerocoTimer = 0f;
        }
    }

    void SpawnKepalaKeroco() {
        // if (enemyType != EnemyType.KepalaKeroco && enemyType != EnemyType.Any) {
        //     return;
        // }

        spawnKepalaKerocoTimer += Time.deltaTime;
        if (spawnKepalaKerocoTimer >= spawnKepalaKerocoInterval)
        {
            SpawnEnemy(kepalaKerocoPrefab);
            spawnKepalaKerocoTimer = 0f;
        }
    }

    void SpawnJenderal() {
        // if (enemyType != EnemyType.Jenderal && enemyType != EnemyType.Any) {
        //     return;
        // }

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
