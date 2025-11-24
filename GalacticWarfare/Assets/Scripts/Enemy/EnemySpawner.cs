using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f; 
    public float spawnRateDecrease = 0.05f; 
    public float minSpawnRate = 0.5f;

    public float minY = -3.5f;
    public float maxY = 3.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnEnemy();
            timer = 0f;

            // Aumenta dificuldade ao longo do tempo
            if (spawnRate > minSpawnRate)
                spawnRate -= spawnRateDecrease;
        }
    }

    void SpawnEnemy()
    {
        Vector3 pos = new Vector3(transform.position.x, Random.Range(minY, maxY), 0);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}