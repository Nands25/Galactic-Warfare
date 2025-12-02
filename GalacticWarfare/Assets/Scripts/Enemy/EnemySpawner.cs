using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab do inimigo")]
    public GameObject enemyPrefab;

    [Header("Quantidade máxima de inimigos na cena")]
    public int maxEnemies = 10;
    private int currentEnemies = 0;

    [Header("Configurações de Spawn")]
    public float spawnRate = 2f;
    public float spawnRateDecrease = 0.05f;
    public float minSpawnRate = 0.5f;

    [Header("Limites de posição Y")]
    public float minY = -3.5f;
    public float maxY = 3.5f;

    private float timer;

    void Update()
    {
        // Só spawna se não tiver atingido o limite de inimigos
        if (currentEnemies >= maxEnemies) return;

        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnEnemy();
            timer = 0f;

            // Dificuldade aumenta com o tempo
            if (spawnRate > minSpawnRate)
                spawnRate -= spawnRateDecrease;
        }
    }

    void SpawnEnemy()
    {
        Vector3 pos = new Vector3(transform.position.x, Random.Range(minY, maxY), 0);
        Instantiate(enemyPrefab, pos, Quaternion.identity);

        currentEnemies++; // contabiliza novo inimigo
    }

    // Chamado quando um inimigo morre
    public void OnEnemyKilled()
    {
        currentEnemies--;
        if (currentEnemies < 0) currentEnemies = 0; // só pra garantir
    }
}