using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints; // Array of spawn points
    public Transform player;
    public Camera playerCamera;


    public int waves = 3;
    public int enemiesPerWave = 3;
    public float timeBetweenWaves = 10f;
    public float timeBetweenEnemies = 2f;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int wave = 0; wave < waves; wave++)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            for (int enemyCount = 0; enemyCount < enemiesPerWave; enemyCount++)
            {
                // Choose a random spawn point
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                SpawnEnemy(randomSpawnPoint);
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
        }
    }

    void SpawnEnemy(Transform spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Access the MonsterController script on the spawned enemy
        MonsterController monsterController = enemy.GetComponent<MonsterController>();

        // Access the Billboard script on the Canvas child
        Billboard billboardScript = enemy.GetComponentInChildren<Billboard>();


        // Check if the MonsterController script is present
        if (monsterController != null)
        {
            // Set the playerCamera attribute
            monsterController.playerCamera = playerCamera;

            billboardScript.cam = player;
        }
        else
        {
            Debug.LogError("MonsterController script not found on the spawned enemy.");
        }
    }
}
