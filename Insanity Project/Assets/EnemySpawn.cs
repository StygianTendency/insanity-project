using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefab; // Array of GameObjects to spawn
    public Transform spawnPoint; // The point where the GameObjects will be spawned
    [SerializeField] private int maxWaves; // Maximum number of waves
    [SerializeField] private int initialSpawnCount; // Number of objects to spawn in the first wave

    private int currentWave = 0; // Counter for the current wave

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (currentWave < maxWaves)
        {
            // Calculate the number of objects to spawn in this wave
            int spawnCount = Mathf.CeilToInt(initialSpawnCount * (1 + 0.2f * currentWave));

            for (int i = 0; i < spawnCount; i++)
            {
                yield return new WaitForSeconds(Random.Range(2, 3)); // Wait for a random time between 2 to 3 seconds

                // Select a random GameObject from the array
                int rand = Random.Range(0, enemyPrefab.Length);
                GameObject enemyToSpawn = enemyPrefab[rand];

                // Instantiate the selected GameObject at the spawn point
                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            }

            Debug.Log("Wave " + (currentWave + 1) + " completed. Spawned " + spawnCount + " objects.");

            currentWave++; // Move to the next wave

            yield return new WaitForSeconds(5); // Wait for 5 seconds before the next wave
        }

        Debug.Log("All waves completed."); // Log when all waves are completed
    }
}