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

    private int currentWaveIndex = 0;
    private bool stopSpawning = false;
    private bool waveInProgress = false;

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

        if (!waveInProgress && AllEnemiesDefeated())
        {
            StartCoroutine(DelayBeforeNextWave());
        }
    }

    private IEnumerator DelayBeforeNextWave()
    {
        yield return new WaitForSeconds(currentWave.PauseTimeAfterThisWaveEnded);
        StartNextWave();
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            currentWave = waves[currentWaveIndex];
            StartCoroutine(SpawnWaveEnemies());
            currentWaveIndex++;
            waveInProgress = true;
        }
        else
        {
            stopSpawning = true;
        }
    }

    private IEnumerator SpawnWaveEnemies()
    {
        int enemiesToSpawn = currentWave.NumberToSpawn;
        int enemiesSpawned = 0;

        while (enemiesSpawned < enemiesToSpawn)
        {
            int randomEnemyIndex = Random.Range(0, currentWave.EnemiesInWave.Length);
            int randomSpawnPointIndex = Random.Range(0, spawnpoints.Length);

            GameObject enemy = Instantiate(currentWave.EnemiesInWave[randomEnemyIndex], spawnpoints[randomSpawnPointIndex].position, Quaternion.identity);

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

            enemiesSpawned++;
            yield return new WaitForSeconds(currentWave.TimeBetweenSpawnEnemiesInsideWave);
        }

        waveInProgress = false;
    }

    private bool AllEnemiesDefeated()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Animal");
        return enemies.Length == 0;
    }
}
