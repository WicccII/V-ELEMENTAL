using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroup; // list group enemy in this wave
        public int waveQuota;//number of enemies spawn in this wave
        public float spawnInterval;//Interval at wich spawn enemies
        public int spawnCount;//number of enemies allready spawn in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;//number of enemies
        public int spawnCount;//number of enemies allready spawn
        public GameObject enemyPrefab;
    }

    public List<wave> waves; // list all wave of game 
    public int currentWaveCount; // number of wave allready spawn
    Transform player;

    [Header("Timer")]
    float spawnTimer;
    public float waveInterval;

    // Start is called before the first frame update
    void Start()
    {
        CaculateWaveQuota();
        player = FindAnyObjectByType<PlayerStats>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer > waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0;
            SpawnedEnemy();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CaculateWaveQuota();
        }
    }

    void CaculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroup)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    void SpawnedEnemy()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroup)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    //spawn enemies in circle
                    float radius = 15f;  // Radius of the circle
                    float angle = Random.Range(0f, 2f * Mathf.PI);  // Random angle from 0 to 2π (360 degrees)

                    float x = Mathf.Cos(angle) * radius;  // Calculate the x-coordinate
                    float y = Mathf.Sin(angle) * radius;  // Calculate the y-coordinate

                    Vector2 spawnPosition = new Vector2(player.position.x + x, player.position.y + y);
                    Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                }
            }
        }
    }
}
