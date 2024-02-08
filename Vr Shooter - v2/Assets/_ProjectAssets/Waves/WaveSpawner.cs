using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform player;
    public Camera playerCamera;

    public TextMeshProUGUI waveText;
    public Canvas waveCanvas;
    public float waveTextDuration = 3f;
    
    private Wave currentWave;

    [SerializeField]
    private Transform[] spawnpoints;

    private int currentWaveIndex = 0;
    private bool stopSpawning = false;
    private bool waveInProgress = false;

    private void Awake()
    {
        waveCanvas.enabled = false; // Disable the canvas initially
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
            UpdateWaveText(currentWaveIndex + 1); // Update wave text with the current wave number
            currentWaveIndex++;
            waveInProgress = true;

            // Enable the canvas at the start of each wave
            waveCanvas.enabled = true;
        }
        else
        {
            stopSpawning = true;
        }
        
    }

    private void UpdateWaveText(int waveNumber)
    {
        if (waveText != null)
        {
            waveText.text = "Wave " + waveNumber.ToString() + ". Get Ready!!";
        }
    }

    private IEnumerator SpawnWaveEnemies()
    {
        waveCanvas.enabled = true; // Show the canvas at the start of the wave

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
        StartCoroutine(HideWaveTextAfterDelay());

    }

    private IEnumerator HideWaveTextAfterDelay()
    {
        yield return new WaitForSeconds(waveTextDuration);
        waveCanvas.enabled = false; // Hide the canvas after the specified duration
    }

    public bool AllEnemiesDefeated()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Animal");
        return enemies.Length == 0;
    }

    public bool AllWavesCompleted()
    {
        return currentWaveIndex >= waves.Length && !waveInProgress;
    }
}
