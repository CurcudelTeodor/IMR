using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform player;
    public Camera playerCamera;
    private Wave currentWave;

    [SerializeField]
    private Transform[] spawnpoints;

    private float timeBtwnSpawns;
    private float timeBetweenEnemiesInWave;
    private int currentWaveIndex = 0;

    private bool stopSpawning = false;

    private void Awake()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (stopSpawning)
        {
            return;
        }

        if (Time.time >= timeBtwnSpawns)
        {
            SpawnWave();
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            currentWave = waves[currentWaveIndex];
            timeBtwnSpawns = Time.time + currentWave.TimeBeforeThisWave;
            timeBetweenEnemiesInWave = currentWave.TimeBetweenSpawnEnemiesInsideWave;
            currentWaveIndex++;
        }
        else
        {
            stopSpawning = true;
        }
    }

    private void SpawnWave()
    {
        StartCoroutine(SpawnWaveEnemies());
    }

    private IEnumerator SpawnWaveEnemies()
    {
        for (int i = 0; i < currentWave.NumberToSpawn; i++)
        {
            int randomEnemyIndex = Random.Range(0, currentWave.EnemiesInWave.Length);
            int randomSpawnPointIndex = Random.Range(0, spawnpoints.Length);

            SpawnEnemy(currentWave.EnemiesInWave[randomEnemyIndex], spawnpoints[randomSpawnPointIndex]);

            yield return new WaitForSeconds(timeBetweenEnemiesInWave);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        MonsterController monsterController = enemy.GetComponent<MonsterController>();
        Billboard billboardScript = enemy.GetComponentInChildren<Billboard>();

        if (monsterController != null)
        {
            monsterController.playerCamera = playerCamera;
            billboardScript.cam = player;
        }
        else
        {
            Debug.LogError("MonsterController script not found on the spawned enemy.");
        }
    }
}
